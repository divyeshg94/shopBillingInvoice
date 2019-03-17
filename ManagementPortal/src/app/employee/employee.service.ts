import { Injectable, OnInit } from '@angular/core';
import { Employee } from '../Model/Employee';
import { inherits } from 'util';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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

export class EmployeeService {
  controller = "employee";

  constructor(
    private http: HttpClient) {
  }

  async getAllEmployees(): Promise<Employee[]> {
    var httpOptions = this.getHttpOptions();
    var url = baseUrl + this.controller + "/getallemployees";
    return await this.http.get<Employee[]>(url, httpOptions).toPromise();
  }

  getHttpOptions() {
    httpOptions.headers = httpOptions.headers.set('Access-Control-Allow-Origin', '*');
    httpOptions.headers = httpOptions.headers.set('Access-Control-Allow-Headers', 'X-Requested-With, Content-Type, Accept, Origin, Authorization')
    // httpOptions.headers = httpOptions.headers.set('Content-Type', 'application/json');
    httpOptions.headers = httpOptions.headers.set('Content-Type', '*');
    httpOptions.headers = httpOptions.headers.set('Allow', 'GET, POST, PUT, DELETE');
    return httpOptions;
  }
}