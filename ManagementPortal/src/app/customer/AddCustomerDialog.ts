import { OnInit } from '@angular/core';
import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { DialogData } from './customer.component';

@Component({
  selector: 'AddCustomerDialog',
  templateUrl: 'addCustomer.html',
})

export class AddCustomerDialog {
  registerForm: FormGroup;
  submitted = false;
  
  constructor(public dialogRef: MatDialogRef<AddCustomerDialog>,
    private formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA)
    public data: DialogData) { }
  onNoClick(): void {
    this.dialogRef.close();
  }
}
