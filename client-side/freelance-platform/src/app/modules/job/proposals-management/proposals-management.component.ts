import { Component } from '@angular/core';
import { ProposalService } from '../services/proposal.service';
import { Proposal } from '../models/proposal.model';
import { Job } from '../models/job.model';
import { MatDialog } from '@angular/material/dialog';
import { JobInfoDialogComponent } from '../jobs-management/dialogs/job-info-dialog/job-info-dialog.component';
import { Router } from '@angular/router';
import { ProposalInfoDialogComponent } from './dialogs/proposal-info-dialog/proposal-info-dialog.component';

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
    private dialog: MatDialog,
    private router: Router) {
      const navigation = this.router.getCurrentNavigation();
      const state = navigation?.extras.state;

      if(state)
        this.job = state['job'] as Job;
    }

  ngOnInit() {
    this.proposalService.getByJobId(this.job.id).subscribe({
      next: (proposals) => this.proposals = proposals
    });
  }

  openJobInfoDialog() {
    this.dialog.open(JobInfoDialogComponent, {
      width: '50%',
      height: '80%',
      data: { job: this.job }
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
