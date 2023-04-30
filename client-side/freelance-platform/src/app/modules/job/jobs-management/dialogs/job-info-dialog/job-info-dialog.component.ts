import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Job } from '../../../models/job.model';
import { EnumConverter } from 'src/app/modules/shared/utils/enum-string-converter.util';

@Component({
  selector: 'app-job-info-dialog',
  templateUrl: './job-info-dialog.component.html',
  styleUrls: ['./job-info-dialog.component.scss']
})
export class JobInfoDialogComponent {

  job: Job;

  constructor(
    public enumConverter: EnumConverter,
    @Inject(MAT_DIALOG_DATA) public data: {job: Job}) {
    this.job = data.job;
  }

}
