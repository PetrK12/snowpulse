import {IOrderItem} from "./orderItem";
import {IAddress} from "./address";
export interface IOrderToCreate {
  basketId: string;
  deliveryMethodId: string;
  shipToAddress: IAddress;
}
export interface IOrder {
  id: number;
  buyerEmail: string;
  orderDate: string
  shipToAddress: IAddress;
  deliveryMethod: string;
  orderItems: IOrderItem[];
  subtotal: number;
  total: number;
  status: string;
}
