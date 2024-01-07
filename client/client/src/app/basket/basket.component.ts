import { Component } from '@angular/core';
import {BasketService} from "./basket.service";
import {IBasketItem} from "../shared/models/basketItem";

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.scss'
})
export class BasketComponent {
  constructor(public basketService: BasketService) {}

  incrementQuantity(item: IBasketItem){
    this.basketService.addItemToBasket(item);
  }
  removeItem(id: number, quantity: number){
    this.basketService.removeItemFromBasket(id, quantity);
  }
}