import {Component, OnInit} from '@angular/core';
import {OrderService} from "./order.service";
import {IOrder} from "../shared/models/order";

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrl: './order.component.scss'
})
export class OrderComponent implements OnInit{
  orders: IOrder[] = [];
  constructor(public orderService: OrderService) {}

  ngOnInit(): void {
    this.getOrders();
  }
  getOrderById(id: number){
    this.orderService.getOrder(1).subscribe((order: IOrder) => {
      console.log(order)
    })
  }
  getOrders(){
    this.orderService.getOrders().subscribe((orders: IOrder[]) => {
      this.orders = orders
    })
  }
}
