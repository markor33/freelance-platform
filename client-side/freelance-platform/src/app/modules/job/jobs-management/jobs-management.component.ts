import { Component, ViewChild } from '@angular/core';
import { JobService } from '../services/job.service';
import { Job } from '../models/job.model';
import { MatDialog } from '@angular/material/dialog';
import { CreateJobDialogComponent } from '../create-job-dialog/create-job-dialog.component';
import { JobInfoDialogComponent } from './dialogs/job-info-dialog/job-info-dialog.component';
import { SnackBarsService } from '../../shared/services/snack-bars.service';
import { DeleteConfirmationDialogComponent } from '../../shared/dialogs/delete-confirmation-dialog/delete-confirmation-dialog.component';
import { Router } from '@angular/router';
import { EnumConverter } from '../../shared/utils/enum-string-converter.util';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-jobs-management',
  templateUrl: './jobs-management.component.html',
  styleUrls: ['./jobs-management.component.scss']
})
export class JobsManagementComponent {

  public hoveredRow: any = null;

  public jobs: MatTableDataSource<any> = new MatTableDataSource();
  public displayedColumns: string[] = ['title', 'numOfProposals', 'interviewing', 'status', 'actions'];

  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private jobService: JobService,
    private dialog: MatDialog,
    private snackBarService: SnackBarsService,
    private router: Router,
    public enumConverter: EnumConverter) { }

  ngAfterViewInit() {
    console.log(this.sort);
    this.jobs.sort = this.sort;
  }

  ngOnInit() {
    this.jobService.getByClient().subscribe({
      next: (jobs) => this.jobs.data = jobs
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
      this.jobs.data = [...this.jobs.data, result];
    });
  }

  openJobInfoDialog(job: Job) {
    this.dialog.open(JobInfoDialogComponent, {
      width: '50%',
      height: '80%',
      data: { jobId: job.id }
    });
  }

  openProposals(job: Job) {
    this.router.navigate(['/job', job.id, 'proposal-management']);
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
    const index = this.jobs.data.indexOf(job);
    this.jobs.data.splice(index, 1);
    this.jobs.data = [...this.jobs.data];
    this.snackBarService.primary('Job deleted successfully');
  }

}
