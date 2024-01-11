import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import { IOrder } from "../shared/models/order";
import {environment} from "../../environments/environment";
import {map} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getOrder(id: number){
    return this.http.get<IOrder>(this.baseUrl + 'orders/'+ id);
  }
  getOrders(){
    return this.http.get<IOrder[]>(this.baseUrl + 'orders')
  }
}
