import {Component, EventEmitter, Input, Output} from '@angular/core';
import {IBasketItem} from "../../models/basketItem";
import {BasketService} from "../../../basket/basket.service";

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrl: './summary.component.scss'
})
export class SummaryComponent {
  @Output() addItem = new EventEmitter<IBasketItem>();
  @Output() removeItem = new EventEmitter<{ id: number, quantity: number }>();
  @Input() isBasket = true;
  constructor(public basketService: BasketService) {}
  addBasketItem(item: IBasketItem){
    this.addItem.emit(item);
  }

  removeBasketItem(id: number, quantity = 1){
    this.removeItem.emit({id, quantity});
  }

}
