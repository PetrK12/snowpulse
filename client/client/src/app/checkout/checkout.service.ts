import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {IDeliveryMethod} from "../shared/models/deliveryMethod";
import {map} from "rxjs";
import {IOrderToCreate, IOrder} from "../shared/models/order";

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getDeliveryMethods() {
    return this.http.get<IDeliveryMethod[]>(this.baseUrl +"orders/deliveryMethods").pipe(
      map(dm => {
        console.log(dm);
        return dm.sort((a, b) => b.price - a.price);
      })
    )
  }
  createOrder(order: IOrderToCreate){
    return this.http.post<IOrder>(this.baseUrl + 'orders', order);
  }
}
