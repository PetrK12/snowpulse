import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import { BehaviorSubject, map } from "rxjs";
import {Basket, IBasket} from "../shared/models/basket";
import {HttpClient} from "@angular/common/http";
import {IBasketItem} from "../shared/models/basketItem";
import {IProduct} from "../shared/models/products";
import {IBasketTotals} from "../shared/models/basketTotals";
import {IDeliveryMethod} from "../shared/models/deliveryMethod";

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<IBasket | null>(null);
  basketSource$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<IBasketTotals | null>(null);
  basketTotalSource$ = this.basketTotalSource.asObservable();
  constructor(private htpp: HttpClient) { }

  createPaymentIntent(){
    return this.htpp.post<IBasket>(this.baseUrl + 'payment/' + this.getCurrentBasketValue()?.id, {})
      .pipe(
        map(basket => {
          this.basketSource.next(basket);
        })
      )
  }
  setShippingPrice(deliverMethody: IDeliveryMethod){
    const basket = this.getCurrentBasketValue();
    if(basket){
      basket.deliveryMethodId = deliverMethody.id;
      basket.shippingPrice = deliverMethody.price;
      this.setBasket(basket);
    }
  }
  getBasket(id: string) {
    this.htpp.get<IBasket>(this.baseUrl + 'basket?id=' + id) .subscribe({
      next: basket => {
        this.basketSource.next(basket)
        this.calculateTotals();
      }
    })
  }
  setBasket(basket: IBasket){
    this.htpp.post<IBasket>(this.baseUrl + 'basket', basket).subscribe({
      next: basket => {
        this.basketSource.next(basket)
        this.calculateTotals();
      }
    })
  }
  getCurrentBasketValue(){
    return this.basketSource.value;
  }

  addItemToBasket(item: IProduct | IBasketItem, quantity = 1){
    if (this.isProduct(item)) item = this.mapProductItemToBasketItem(item);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, item, quantity);
    this.setBasket(basket);
  }

  removeItemFromBasket(id: number, quantity = 1){
    const basket = this.getCurrentBasketValue();
    if(!basket) return;
    const item = basket.items.find(x => x.id === id);
    if(item) {
      item.quantity -= quantity;
      if(item.quantity === 0){
        basket.items = basket.items.filter(x => x.id !== id);
      }
      if (basket.items.length > 0) this.setBasket(basket);
      else this.deleteBasket(basket);
    }
  }

  private deleteBasket(basket: IBasket){
    return this.htpp.delete(this.baseUrl + 'basket?id=' + basket.id).subscribe({
      next: () => {
        this.deleteLocalBasket()
      }
    })
  }

  deleteLocalBasket(){
    this.basketSource.next(null);
    this.basketTotalSource.next(null);
    localStorage.removeItem('basket_id')
  }
  private createBasket(): IBasket{
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }
  private mapProductItemToBasketItem(item: IProduct): IBasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      quantity: 0,
      pictureUrl: item.pictureUrl,
      brand: item.productBrand,
      type: item.productType
    }
  }
  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const item = items.find(x => x.id == itemToAdd.id);
    if (item) item.quantity += quantity;
    else{
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    return items;
  }
  private calculateTotals() {
    const basket = this.getCurrentBasketValue();
    if(!basket) return;
    const subtotal = basket.items.reduce((a, b) =>(b.price * b.quantity) + a, 0);
    const total = subtotal + basket.shippingPrice;
    this.basketTotalSource.next({shipping: basket.shippingPrice, total, subtotal})
  }

  private isProduct(item: IProduct | IBasketItem): item is IProduct {
    return (item as IProduct).productBrand !== undefined;
  }
}
