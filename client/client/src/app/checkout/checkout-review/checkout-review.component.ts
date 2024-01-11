import {Component, Input} from '@angular/core';
import {BasketService} from "../../basket/basket.service";
import {ToastrService} from "ngx-toastr";
import {CdkStepper} from "@angular/cdk/stepper";

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrl: './checkout-review.component.scss'
})
export class CheckoutReviewComponent {
  @Input() appStepper?: CdkStepper;

  constructor(private basketService: BasketService, private toastr: ToastrService) {}

  createPyamentIntent() {
    this.basketService.createPaymentIntent().subscribe({
      next: value => {
        this.appStepper?.next();
      }
    })
  }
}
