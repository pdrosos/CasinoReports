import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MaterialModule } from '@shared/material.module';

import { MaterialFileInputModule } from 'ngx-material-file-input';

import { FlexmonsterPivotModule } from 'ng-flexmonster';

@NgModule({
  imports: [
    CommonModule,
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    MaterialFileInputModule,
    FlexmonsterPivotModule,
  ],
  declarations: [],
})
export class SharedModule { }
