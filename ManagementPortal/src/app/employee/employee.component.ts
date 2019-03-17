import { Component, OnInit } from '@angular/core';
import { EmployeeService } from './employee.service';
import { MatTableDataSource } from '@angular/material';
import { Employee } from '../Model/Employee';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {
  totalColumns: string[] = ['Id', 'Name', 'PhoneNumber', 'JoinedOn', 'ReleavedOn', 'IsExists'];
  displayedColumns = ['Id', 'Name', 'PhoneNumber', 'JoinedOn', 'ReleavedOn', 'IsExists'];
  dataSource = new MatTableDataSource<Employee>();
  
  constructor(private employeeService: EmployeeService) { }

  ngOnInit() {
    this.getAllEmployees();
  }

  getAllEmployees() {
    let employees = this.employeeService.getAllEmployees().then(employees => {
      this.dataSource = new MatTableDataSource(employees);
    });
  }
}
