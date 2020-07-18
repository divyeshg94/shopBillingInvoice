import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppComponent } from '../app.component';
import { environment } from 'src/environments/environment';
import { Item } from '../Model/Item';
import { CustomerService } from '../customer/customer.service';

const baseUrl = environment.apiBaseUrl;

@Injectable({
  providedIn: 'root'
})
export class ItemService {
  controller = "items";

  constructor(private http: HttpClient, private customerService: CustomerService) { }

  async getAllItems(): Promise<Item[]> {
    var httpOptions = this.customerService.getHttpOptions();
    var url = baseUrl + this.controller;
    return await this.http.get<Item[]>(url, httpOptions).toPromise();
  }

  async getItem(name: string, category: string): Promise<Item[]> {
    var httpOptions = this.customerService.getHttpOptions();
    var query = "name=" + name + "&category="+category;
    var url = baseUrl + this.controller + query;
    return await this.http.get<Item[]>(url, httpOptions).toPromise();
  }

  async addItem(item :Item){
    var httpOptions = this.customerService.getHttpOptions();
    var url = baseUrl + this.controller;
    return await this.http.post(url, item, httpOptions).toPromise();
  }

  async updateItem(item : Item) {
    var httpOptions = this.customerService.getHttpOptions();
    var url = baseUrl + this.controller;
    return await this.http.put(url, item, httpOptions).toPromise();
  }
}
