import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { CustomerVisitsReport } from '@admin/models/customer-visits-report';

@Component({
  selector: 'app-customer-visits-report-form',
  templateUrl: './customer-visits-report-form.component.html',
})
export class CustomerVisitsReportFormComponent implements OnInit {
  @Input() public customerVisitsReport?: CustomerVisitsReport;

  @Input() public saving$: Observable<boolean>;

  @Input() public serverErrors$: Observable<string[]>;

  @Output() public formSubmit: EventEmitter<FormData>;

  @Output() public formCancel: EventEmitter<null>;

  public form: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.formSubmit = new EventEmitter();
    this.formCancel = new EventEmitter();
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      name: [
        this.customerVisitsReport ? this.customerVisitsReport.name : '',
        [Validators.required, Validators.maxLength(100)],
      ],
    });
  }

  public onSubmit() {
    if (this.form.valid) {
      this.formSubmit.emit(this.form.value);
    }
  }

  public onCancel() {
    this.formCancel.emit();
  }
}
