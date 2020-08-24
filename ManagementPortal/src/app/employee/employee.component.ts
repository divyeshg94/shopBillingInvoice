import { Component, OnInit } from '@angular/core';
import { EmployeeService } from './employee.service';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { Employee } from '../Model/Employee';
import { AddEmployeeDialog } from './AddEmployeeDialog';
import { FormControl } from '@angular/forms';
import { EmployeeAttendanceService } from './employeeAttendance.service';
import { ToastrService } from 'ngx-toastr';

export interface DialogData {
  phoneNumber: string;
  name: string;
}

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {
  totalColumns: string[] = ['Id', 'Name', 'PhoneNumber', 'JoinedOn', 'ReleavedOn', 'IsExists', 'actions'];
  displayedColumns = ['Id', 'Name', 'PhoneNumber', 'JoinedOn', 'ReleavedOn', 'IsExists', 'actions'];
  dataSource = new MatTableDataSource<Employee>();
  phoneNumber: string;
  name: string;
  
  constructor(private employeeService: EmployeeService,
              private attendanceService: EmployeeAttendanceService,
              private toastr: ToastrService,
              public dialog: MatDialog) { }

  ngOnInit() {
    this.getAllEmployees();
  }

  getAllEmployees() {
    let employees = this.employeeService.getAllEmployees().then(employees => {
      this.dataSource = new MatTableDataSource(employees);
    });
  }

  openAddDialog(): void {
    var currentDate = new Date();
    const dialogRef = this.dialog.open(AddEmployeeDialog, {
      width: '400px',
      data: {Name: this.name, PhoneNumber: this.phoneNumber, JoinedOn: currentDate}
    });

    dialogRef.afterClosed().subscribe(result => {
      result.IsExists = true;
      this.employeeService.addEmployee(result).then(employees => {
        this.getAllEmployees();
      });
    });
  }

  openEditDialog(employee: Employee): void{
    employee.isEditMode = true;
    const dialogRef = this.dialog.open(AddEmployeeDialog, {
      width: '400px',
      data: employee
    });

    dialogRef.afterClosed().subscribe(result => {
      this.employeeService.updateEmployee(result).then(employees => {
        this.getAllEmployees();
      });
    });
  }

  checkInEmployee(employee: Employee){
    this.attendanceService.checkIn(employee.Id).then(employees => {
      this.toastr.success("Employee Check-in success!!");
    });
  }

  checkOutEmployee(employee: Employee){
    this.attendanceService.checkOut(employee.Id).then(employees => {
      this.toastr.success("Employee Check-out success!!");
    });
  }
}