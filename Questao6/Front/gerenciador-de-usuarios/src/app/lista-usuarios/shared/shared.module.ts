import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component'; 


@NgModule({
  imports: [CommonModule, MatDialogModule],
  declarations: [ConfirmDialogComponent],
  exports: [ConfirmDialogComponent], 
})
export class SharedModule { }
