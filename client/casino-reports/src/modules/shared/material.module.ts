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
];

@NgModule({
  imports: materialModules,
  exports: materialModules
})

export class MaterialModule { }
