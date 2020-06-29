import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Customer } from '../Model/Customer';


const baseUrl = environment.apiBaseUrl;
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'my-auth-token'
  })
};

@Injectable({
  providedIn: 'root'
})

export class CustomerService {
  controller = "Customer";

  constructor(
    private http: HttpClient) {
  }

  async getAllCustomers(): Promise<Customer[]> {
    var httpOptions = this.getHttpOptions();
    var url = baseUrl + this.controller + "/GetAll";
    return await this.http.get<Customer[]>(url, httpOptions).toPromise();
  }

  async addCustomer(customer : Customer) {
    var httpOptions = this.getHttpOptions();
    var url = baseUrl + this.controller + "/Add";
    return await this.http.post(url, customer, httpOptions).toPromise();
  }

  async updateCustomer(customer : Customer) {
    var httpOptions = this.getHttpOptions();
    var url = baseUrl + this.controller + "/update";
    return await this.http.put(url, customer, httpOptions).toPromise();
  }

  getHttpOptions() {
    httpOptions.headers = httpOptions.headers.set('Access-Control-Allow-Origin', '*');
    httpOptions.headers = httpOptions.headers.set('Access-Control-Allow-Headers', 'X-Requested-With, Content-Type, Accept, Origin, Authorization')
    httpOptions.headers = httpOptions.headers.set('Content-Type', 'application/json');
    //httpOptions.headers = httpOptions.headers.set('Content-Type', '*');
    httpOptions.headers = httpOptions.headers.set('Allow', 'GET, POST, PUT, DELETE');
    httpOptions.headers = httpOptions.headers.set('Access-Control-Allow-Methods', 'GET,POST,OPTIONS,DELETE,PUT');
    return httpOptions;
  }
}