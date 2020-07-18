import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Invoice } from '../Model/Invoice';
import { AppComponent } from '../app.component';
import { environment } from 'src/environments/environment';

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

export class InvoiceService {
  controller = "invoice";
  
  constructor(private http: HttpClient) { }

  async getAllInvoices(): Promise<Invoice[]> {
    var httpOptions = this.getHttpOptions();
    var url = baseUrl + this.controller;
    return await this.http.get<Invoice[]>(url, httpOptions).toPromise();
  }

  async getInvoicesById(invoiceId: number): Promise<Invoice> {
    var httpOptions = this.getHttpOptions();
    var url = baseUrl + this.controller + "?id=" + invoiceId;
    return await this.http.get<Invoice>(url, httpOptions).toPromise();
  }

  async getInvoicesByCustomer(customerId: number): Promise<Invoice[]> {
    var httpOptions = this.getHttpOptions();
    var url = baseUrl + this.controller + "?customerId=" + customerId;
    return await this.http.get<Invoice[]>(url, httpOptions).toPromise();
  }

  async getInvoicesByEmpoyee(employeeId: number): Promise<Invoice[]> {
    var httpOptions = this.getHttpOptions();
    var url = baseUrl + this.controller + "?employeeid=" + employeeId;
    return await this.http.get<Invoice[]>(url, httpOptions).toPromise();
  }

  async sendInvoiceEmail(invoiceId: number) {
    var httpOptions = this.getHttpOptions();
    var url = baseUrl + this.controller + "/SendMail?invoiceId=" + invoiceId;
    return await this.http.get(url, httpOptions).toPromise();
  }

  async addInvoice(invoice :Invoice){
    var httpOptions = this.getHttpOptions();
    var url = baseUrl + this.controller;
    return await this.http.post(url, invoice, httpOptions).toPromise();
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
