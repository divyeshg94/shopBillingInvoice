import { Injectable, OnInit } from '@angular/core';
import { Employee } from '../Model/Employee';
import { inherits } from 'util';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { toDate } from '@angular/common/src/i18n/format_date';
import { EmployeeAttendance, EmployeeWorkingResult } from '../Model/EmployeeAttendance';


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

export class EmployeeAttendanceService {
  controller = "attendance";

  constructor(
    private http: HttpClient) {
  }

  async checkIn(employeeId : number) {
    var httpOptions = this.getHttpOptions();
    var url = baseUrl + this.controller + "/checkin?employeeId=" + employeeId;
    return await this.http.post(url, null, httpOptions).toPromise();
  }

  async checkOut(employeeId : number) {
    var httpOptions = this.getHttpOptions();
    var url = baseUrl + this.controller + "/checkout?employeeId=" + employeeId;
    return await this.http.post(url, null, httpOptions).toPromise();
  }

  async getAll(employeeId : number) : Promise<EmployeeAttendance[]>{
    var httpOptions = this.getHttpOptions();
    var url = baseUrl + this.controller + "/attendance?employeeId=" + employeeId;
    return await this.http.get<EmployeeAttendance[]>(url, httpOptions).toPromise();
  }

  async getWorkingDays(employeeId: number, fromDate : Date, endDate : Date): Promise<EmployeeWorkingResult>{
    var httpOptions = this.getHttpOptions();
    var url = baseUrl + this.controller + "/attendance/workingDays?employeeId=" + employeeId +"&startTime=" + fromDate + "&endTime=" + endDate;
    return await this.http.get<EmployeeWorkingResult>(url, httpOptions).toPromise();
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