import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { DialogData } from './customer.component';

@Component({
  selector: 'AddCustomerDialog',
  templateUrl: 'addCustomer.html',
})

export class AddCustomerDialog {
  constructor(public dialogRef: MatDialogRef<AddCustomerDialog>,
    @Inject(MAT_DIALOG_DATA)
    public data: DialogData) { }
  onNoClick(): void {
    this.dialogRef.close();
  }
}
