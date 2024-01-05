import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  title = 'SnowPulse';
  products: any[]= [];
  constructor(private http: HttpClient) {}

  ngOnInit(): void{
    this.http.get('https://localhost:5001/api/products?pageSize=50').subscribe({
      next: (response: any) => this.products = response.data,
      error: error => console.log(error),
      complete: () => {
        console.log('request completed');
        console.log('placholder');
      }
    })
  }
}
