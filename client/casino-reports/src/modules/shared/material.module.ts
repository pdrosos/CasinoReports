import { NgModule } from '@angular/core';
import {
  MatSidenavModule,
  MatToolbarModule,
  MatCardModule,
  MatListModule,
  MatInputModule,
  MatSelectModule,
  MatButtonModule,
  MatIconModule,
  MatTableModule,
  MatSortModule,
  MatSnackBarModule,
  MatProgressSpinnerModule,
  MatDialogModule,
} from '@angular/material';

const materialModules: any[] = [
  MatSidenavModule,
  MatToolbarModule,
  MatCardModule,
  MatListModule,
  MatInputModule,
  MatSelectModule,
  MatButtonModule,
  MatIconModule,
  MatTableModule,
  MatSortModule,
  MatSnackBarModule,
  MatProgressSpinnerModule,
  MatDialogModule,
];

@NgModule({
  imports: materialModules,
  exports: materialModules
})

export class MaterialModule { }
