import {Component, OnInit} from '@angular/core';
import {FormBuilder, Validators} from "@angular/forms";
import {AccountService} from "../account/account.service";
import {add} from "ngx-bootstrap/chronos";
import {IAddress} from "../shared/models/address";
import {BasketService} from "../basket/basket.service";

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss'
})
export class CheckoutComponent implements OnInit{

  constructor(private fb: FormBuilder, private accountService: AccountService,
              private basketService: BasketService) {}

  checkoutForm = this.fb.group({
    addressForm: this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      street: ['', Validators.required],
      city: ['', Validators.required],
      zipCode: ['', Validators.required]
    }),
    deliveryForm: this.fb.group({
      deliveryMethod: ['', Validators.required],
    }),
    paymentForm: this.fb.group({
      nameOnCard: ['', Validators.required],
    })
  })
  getAddressFormValues() {
    this.accountService.getUserAddress().subscribe({
      next: address => {
        address && this.checkoutForm.get('addressForm')?.patchValue(address);
      }
    })
  }
  updateAddress(address: IAddress){
    this.accountService.updateUserAddress(address);
  }
  ngOnInit() {
    this.getAddressFormValues();
    this.getDeliverMethodValue();
  }

  getDeliverMethodValue() {
    const basket = this.basketService.getCurrentBasketValue();
    if(basket && basket.deliveryMethodId){
      this.checkoutForm.get('deliveryForm')?.get('deliveryMethod')?.patchValue(basket.deliveryMethodId.toString());
    }
  }
}
