import {IBasketItem} from "./basketItem";
import cuid from "cuid";

export interface IBasket {
  id: string;
  items: IBasketItem[];
  clientSecret?: string;
  paymentIntentId?: string;
  deliveryMethodId?: number;
  shippingPrice: number;
}

export class Basket implements IBasket {
  id = cuid();
  items: IBasketItem[]= [];
  shippingPrice = 0;
}
