import { Component } from '@angular/core';
import {IOrder} from "../../shared/models/order";
import {Router} from "@angular/router";

@Component({
  selector: 'app-checkout-success',
  templateUrl: './checkout-success.component.html',
  styleUrl: './checkout-success.component.scss'
})
export class CheckoutSuccessComponent {
  order?:IOrder;
  ngOnInit(): void {


  }
  constructor(private router:Router) {
    const nav= this.router.getCurrentNavigation();
    this.order=nav?.extras?.state as IOrder;

  }
}
