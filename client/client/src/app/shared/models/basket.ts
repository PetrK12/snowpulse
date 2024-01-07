import {IBasketItem} from "./basketItem";
import cuid from "cuid";

export interface IBasket {
  id: string;
  items: IBasketItem[];
}

export class Basket implements IBasket {
  id = cuid();
  items: IBasketItem[]= [];

}
