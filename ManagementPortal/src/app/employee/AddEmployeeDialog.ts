import { Component, Inject } from '@angular/core';
    import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { DialogData } from './employee.component';

@Component({
  selector: 'AddEmployeeDialog',
  templateUrl: 'addEmployee.html',
})

export class AddEmployeeDialog {
  constructor(public dialogRef: MatDialogRef<AddEmployeeDialog>,
    @Inject(MAT_DIALOG_DATA)
    public data: DialogData) { }
  onNoClick(): void {
    this.dialogRef.close();
  }
}
