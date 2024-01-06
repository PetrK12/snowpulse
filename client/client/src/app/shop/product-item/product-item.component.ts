import {Component, Input} from '@angular/core';
import {IProduct} from "../../shared/models/products";

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.scss'
})
export class ProductItemComponent {
  @Input() product?: IProduct;
}
