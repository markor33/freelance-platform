import { Component } from '@angular/core';
import { ProposalService } from '../services/proposal.service';
import { Proposal } from '../models/proposal.model';
import { Job } from '../models/job.model';
import { MatDialog } from '@angular/material/dialog';
import { JobInfoDialogComponent } from '../jobs-management/dialogs/job-info-dialog/job-info-dialog.component';
import { ActivatedRoute, Router } from '@angular/router';
import { ProposalInfoDialogComponent } from './dialogs/proposal-info-dialog/proposal-info-dialog.component';
import { JobService } from '../services/job.service';

@Component({
  selector: 'app-proposals-management',
  templateUrl: './proposals-management.component.html',
  styleUrls: ['./proposals-management.component.scss']
})
export class ProposalsManagementComponent {

  job: Job = new Job();
  proposals: Proposal[] = [];

  public displayedColumns: string[] = ['freelancer', 'location', 'date'];

  constructor(
    private proposalService: ProposalService,
    private jobService: JobService,
    private dialog: MatDialog,
    private router: Router,
    private route: ActivatedRoute) {
      const jobId = this.route.snapshot.paramMap.get('id') as string;
      this.jobService.get(jobId).subscribe((job) => this.job = job);
      this.proposalService.getByJobId(jobId).subscribe((proposals) => this.proposals = proposals);
    }
    
  openJobInfoDialog() {
    this.dialog.open(JobInfoDialogComponent, {
      width: '50%',
      height: '80%',
      data: { jobId: this.job.id }
    });
  }

  openProposalInfoDialog(proosal: Proposal) {
    this.dialog.open(ProposalInfoDialogComponent, {
      width: '50%',
      height: '80%',
      data: { proposal: proosal }
    })
  }

  openFreelancerProfile(freelancerId: string) {
    this.router.navigate([`freelancer/profile/${freelancerId}`]);
  }

}
