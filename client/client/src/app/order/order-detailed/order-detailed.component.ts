import {Component, OnInit} from '@angular/core';
import {IOrder} from "../../shared/models/order";
import {HttpClient} from "@angular/common/http";
import {OrderService} from "../order.service";
import {ActivatedRoute} from "@angular/router";
import {BreadcrumbService} from "xng-breadcrumb";

@Component({
  selector: 'app-order-detailed',
  templateUrl: './order-detailed.component.html',
  styleUrl: './order-detailed.component.scss'
})
export class OrderDetailedComponent implements OnInit{
  order?: IOrder;

  constructor(private route: ActivatedRoute, private breadcrumbService: BreadcrumbService, private orderService: OrderService) {
    this.breadcrumbService.set('@OrderDetailed', '');
  }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if(id){
      this.orderService.getOrder(Number.parseInt(id))
        .subscribe((order: IOrder) => {
          this.order = order;
          this.breadcrumbService.set('@OrderDetailed', `Order# ${order.id} - ${order.status}`);
        });
    }
  }
}
