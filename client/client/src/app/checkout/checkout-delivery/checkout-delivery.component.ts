import {Component, Input, OnInit} from '@angular/core';
import {FormGroup} from "@angular/forms";
import {IDeliveryMethod} from "../../shared/models/deliveryMethod";
import {CheckoutService} from "../checkout.service";
import {BasketService} from "../../basket/basket.service";

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrl: './checkout-delivery.component.scss'
})
export class CheckoutDeliveryComponent implements OnInit{
  @Input() checkoutForm?: FormGroup;
  deliveryMethods: IDeliveryMethod[] = [];

  constructor(private checkoutService: CheckoutService, private basketService: BasketService) {
  }

  ngOnInit(): void {
    this.checkoutService.getDeliveryMethods().subscribe({
      next: dm => {this.deliveryMethods = dm; console.log(dm)}
    })
  }

  setShippingPrice(deliveryMethod: IDeliveryMethod){
    this.basketService.setShippingPrice(deliveryMethod);
  }
}
