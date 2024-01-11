import {Component, ElementRef, Input, OnInit, ViewChild} from '@angular/core';
import {FormGroup} from "@angular/forms";
import {BasketService} from "../../basket/basket.service";
import {CheckoutService} from "../checkout.service";
import {ToastrService} from "ngx-toastr";
import {IBasket} from "../../shared/models/basket";
import {IAddress} from "../../shared/models/address";
import {NavigationExtras, Router} from "@angular/router";
import {
  loadStripe,
  Stripe,
  StripeCardCvcElement,
  StripeCardExpiryElement,
  StripeCardNumberElement
} from "@stripe/stripe-js";
import {firstValueFrom} from "rxjs";
import {IOrderToCreate} from "../../shared/models/order";

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrl: './checkout-payment.component.scss'
})
export class CheckoutPaymentComponent implements OnInit{
  @Input() checkoutForm?: FormGroup;
  @ViewChild('cardNumber') cardNumberElement?: ElementRef;
  @ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
  @ViewChild('cardCvc') cardCvcElement?: ElementRef;
  stripe: Stripe | null = null;
  cardNumber?: StripeCardNumberElement;
  cardExpiry?: StripeCardExpiryElement;
  cardCvc?: StripeCardCvcElement;
  cardErrors: any;
  loading = false;
  cardNumberComplete = false;
  cardExpiryComplete = false;
  cardCvcComplete = false;
  constructor(private basketService: BasketService, private checkoutService: CheckoutService,
              private toastr: ToastrService, private router: Router) {}
  async submitOrder() {
    this.loading = true;
    const basket = this.basketService.getCurrentBasketValue();
    try {
      const createdOrder = await this.createOrder(basket);
      const paymentResult = await this.confirmPaymentWithStripe(basket);
      if(paymentResult.paymentIntent){
        this.basketService.deleteLocalBasket();
        const navigationExtras: NavigationExtras = {state: createdOrder};
        this.router.navigate(['checkout/success'], navigationExtras);
      } else {
        this.toastr.error(paymentResult.error.message);
      }
    } catch (error: any) {
      console.log(error);
      this.toastr.error(error.message);
    } finally {
      this.loading = false;
    }
  }
  private async createOrder(basket: IBasket | null){
    if(!basket) throw new Error('Basket is null');
    const orderToCreate = this.getOrderToCreate(basket);
    return firstValueFrom(this.checkoutService.createOrder(orderToCreate));
  }

  private async confirmPaymentWithStripe(basket: IBasket | null) {
    if(!basket) throw new Error('Basket is null');
    const result = this.stripe?.confirmCardPayment(basket.clientSecret!, {
      payment_method: {
        card: this.cardNumber!,
        billing_details: {
          name: this.checkoutForm?.get('paymentForm')?.get('nameOnCard')?.value
        }
      }
    })
    if(!result) throw new Error('Problem attepmtipng payment with stripe')
    return result;
  }
  getOrderToCreate(basket: IBasket) : IOrderToCreate{
    const deliveryMethodId = this.checkoutForm?.get('deliveryForm')?.get('deliveryMethod')?.value;
    const shipToAddress = this.checkoutForm?.get('addressForm')?.value as IAddress;
    if(!deliveryMethodId || !shipToAddress) throw new Error('Problem with basket');
    return {
      basketId: basket.id,
      deliveryMethodId: deliveryMethodId,
      shipToAddress: shipToAddress
    }
  }

  ngOnInit(): void {
    loadStripe('pk_test_51OWxWrIuOwR8hkBmkR2tdfhyKODCVZbm0J8cXkUS2FPwQHDdGL5kkhjrcFr5XuS7OfnX4OB6bD3Y5PRs5cs83Tpd00yNcKIwIz')
      .then(stripe => {
        this.stripe = stripe;
        const elements = stripe?.elements();
        if(elements){
          this.cardNumber = elements.create('cardNumber');
          this.cardNumber.mount(this.cardNumberElement?.nativeElement);
          this.cardNumber.on('change', event =>{
            this.cardNumberComplete = event.complete
            if(event.error) this.cardErrors = event.error.message;
            else this.cardErrors = null;
          })

          this.cardExpiry = elements.create('cardExpiry');
          this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);
          this.cardExpiry.on('change', event =>{
            this.cardExpiryComplete = event.complete
            if(event.error) this.cardErrors = event.error.message;
            else this.cardErrors = null;
          })

          this.cardCvc = elements.create('cardCvc');
          this.cardCvc.mount(this.cardCvcElement?.nativeElement);
          this.cardCvc.on('change', event =>{
            this.cardCvcComplete = event.complete
            if(event.error) this.cardErrors = event.error.message;
            else this.cardErrors = null;
          })
        }
      })
  }

  get paymentFormComplete() {
    return this.checkoutForm?.get('paymentForm')?.valid &&
      this.cardNumberComplete &&
      this.cardExpiryComplete && this.cardCvcComplete
  }
}
