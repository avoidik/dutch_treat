import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from './product';
import { Injectable } from '@angular/core';
import { Order, OrderItem } from './order';
import 'rxjs/add/operator/map';

@Injectable()
export class DataService {
    constructor(private http: HttpClient) { }

    private token: string = "";
    private tokenExp: Date;

    public products: Product[] = [];
    public order: Order = new Order();

    public loadProducts(): Observable<boolean> {
        return this.http.get('/api/products')
            .map((data: any[]) => {
                this.products = data;
                return true;
            });
    }

    public get loginRequired(): boolean {
        return this.token.length == 0 || this.tokenExp > new Date();
    }

    login(creds): Observable<boolean> {
        return this.http
            .post("/account/createtoken", creds)
            .map((data: any) => {
                this.token = data.token;
                this.tokenExp = data.expiration;
                return true;
            });
    }

    checkout() {
        if (!this.order.orderNumber) {
            this.order.orderNumber = this.order.orderDate.getFullYear().toString() + this.order.orderDate.getTime().toString();
        }
        return this.http.post("/api/orders", this.order, {
            headers: new HttpHeaders().set("Authorization", "Bearer " + this.token)
        })
            .map(response => {
                this.order = new Order();
                return true;
            });
    }

    public AddToOrder(product: Product) {
        let item: OrderItem = this.order.items.find(i => i.productId == product.id);

        if (item) {

            item.quantity++;

        } else {

            item = new OrderItem();
            item.productId = product.id;
            item.productArtist = product.artist;
            item.productCategory = product.category;
            item.productArtId = product.artId;
            item.productTitle = product.title;
            item.productSize = product.size;
            item.unitPrice = product.price;
            item.quantity = 1;

            this.order.items.push(item);

        }
    }
}