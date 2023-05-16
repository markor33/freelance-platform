import { Component } from '@angular/core';
import { JobService } from '../services/job.service';
import { Job } from '../models/job.model';
import { MatDialog } from '@angular/material/dialog';
import { ApplyDialogComponent } from '../apply-dialog/apply-dialog.component';
import { CreateJobDialogComponent } from '../create-job-dialog/create-job-dialog.component';

@Component({
  selector: 'app-job-search',
  templateUrl: './job-search.component.html',
  styleUrls: ['./job-search.component.scss']
})
export class JobSearchComponent {

  jobs: Job[] = [];

  constructor(private jobService: JobService, private dialog: MatDialog) { }

  ngOnInit() {
    this.jobService.getAll().subscribe({
      next: (jobs) => this.jobs = jobs
    });
  }

  apply(job: Job) {
    console.log(job);
    this.dialog.open(ApplyDialogComponent, {
      width: '50%',
      height: '80%',
      data: { job: job }
    })
  }

  createJob() {
    this.dialog.open(CreateJobDialogComponent, {
      width: '50%',
      height: '65%',
    })
  }

}
