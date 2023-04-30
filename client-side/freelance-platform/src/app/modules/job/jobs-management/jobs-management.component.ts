import { Component } from '@angular/core';
import { JobService } from '../services/job.service';
import { Job } from '../models/job.model';
import { MatDialog } from '@angular/material/dialog';
import { CreateJobDialogComponent } from '../create-job-dialog/create-job-dialog.component';
import { JobInfoDialogComponent } from './dialogs/job-info-dialog/job-info-dialog.component';
import { SnackBarsService } from '../../shared/services/snack-bars.service';
import { DeleteConfirmationDialogComponent } from '../../shared/dialogs/delete-confirmation-dialog/delete-confirmation-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-jobs-management',
  templateUrl: './jobs-management.component.html',
  styleUrls: ['./jobs-management.component.scss']
})
export class JobsManagementComponent {

  public hoveredRow: any = null;

  public jobs: Job[] = [];
  public displayedColumns: string[] = ['title', 'numOfProposals', 'interviewing', 'actions'];

  constructor(
    private jobService: JobService,
    private dialog: MatDialog,
    private snackBarService: SnackBarsService,
    private router: Router) { }

  ngOnInit() {
    this.jobService.getByClient().subscribe({
      next: (jobs) => this.jobs = jobs
    });
  }

  openCreateJobDialog() {
    const dialog = this.dialog.open(CreateJobDialogComponent, {
      width: '50%',
      height: '80%'
    });
    dialog.afterClosed().subscribe(result => {
      if (!result)
        return;
      this.jobs = [...this.jobs, result];
    });
  }

  openJobInfoDialog(job: Job) {
    this.dialog.open(JobInfoDialogComponent, {
      width: '50%',
      height: '80%',
      data: { job: job }
    });
  }

  openProposals(job: Job) {
    this.router.navigate(['proposal-management'], { state: { job: job }});
  }

  deleteJob(job: Job) {
    const confirmDialog = this.dialog.open(DeleteConfirmationDialogComponent, {
      width: "20%",
      height: "15%"
    });
    confirmDialog.afterClosed().subscribe((res) => {
      console.log(res);
      if (!res)
        return;
      this.jobService.delete(job.id).subscribe({
        complete: () => this.jobDeletedSuccessfully(job)
      });
    });
  }

  jobDeletedSuccessfully(job: Job) {
    const index = this.jobs.indexOf(job);
    this.jobs.splice(index, 1);
    this.jobs = [...this.jobs];
    this.snackBarService.primary('Job deleted successfully');
  }

}
