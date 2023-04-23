import { Component } from '@angular/core';
import { JobService } from '../services/job.service';
import { Job } from '../models/job.model';
import { MatDialog } from '@angular/material/dialog';
import { ApplyDialogComponent } from '../apply-dialog/apply-dialog.component';

@Component({
  selector: 'app-job-listing',
  templateUrl: './job-listing.component.html',
  styleUrls: ['./job-listing.component.scss']
})
export class JobListingComponent {

  jobs: Job[] = [];

  constructor(private jobService: JobService, private dialog: MatDialog) { }

  ngOnInit() {
    this.jobService.getAll().subscribe({
      next: (jobs) => console.log(jobs)
    });
  }

  apply(job: Job) {
    this.dialog.open(ApplyDialogComponent, {
      width: '50%',
      height: '80%',
      data: { job: job }
    })
  }

}
