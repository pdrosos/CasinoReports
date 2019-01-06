import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { FileValidator, FileInput } from 'ngx-material-file-input';

import { CustomerVisitsCollection } from '@admin/models/customer-visits-collection';

@Component({
  selector: 'app-customer-visits-import-form',
  templateUrl: './customer-visits-import-form.component.html',
})
export class CustomerVisitsImportFormComponent implements OnInit {
  @Input() public customerVisitsCollections$: Observable<CustomerVisitsCollection[]>;

  @Input() public maxFileSize: number;

  @Input() public uploading$: Observable<boolean>;

  @Input() public serverErrors$: Observable<string[]>;

  @Output() public formSubmit: EventEmitter<FormData>;

  public form: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.formSubmit = new EventEmitter();
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      customerVisitsCollectionIds: [
        '',
        [Validators.required],
      ],
      name: [
        '',
        [Validators.required, Validators.maxLength(100)],
      ],
      customerVisits: [
        '',
        [Validators.required, FileValidator.maxContentSize(this.maxFileSize)]
      ]
    });
  }

  public onSubmit() {
    if (this.form.valid) {
      const data = this.getFormData();
      this.formSubmit.emit(data);
    }
  }

  private getFormData(): FormData {
    const formData = new FormData();

    const customerVisitsCollectionIds: string[] = this.form.get('customerVisitsCollectionIds').value;
    customerVisitsCollectionIds.forEach((id) => {
      formData.append('customerVisitsCollectionIds[]', id);
    });

    formData.append('name', this.form.get('name').value);

    const fileInput: FileInput = this.form.get('customerVisits').value;
    formData.append('customerVisits', fileInput.files[0]);

    return formData;
  }
}
