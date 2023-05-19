import { Component, Input } from '@angular/core';
import { FreelancerAcceptProposalCommand } from 'src/app/modules/job/models/commands/freelancer-accept-proposal-command.model';
import { Proposal, ProposalStatus } from 'src/app/modules/job/models/proposal.model';
import { ProposalService } from 'src/app/modules/job/services/proposal.service';
import { SnackBarsService } from 'src/app/modules/shared/services/snack-bars.service';

@Component({
  selector: 'app-freelancer-accept',
  templateUrl: './freelancer-accept.component.html',
  styleUrls: ['./freelancer-accept.component.scss']
})
export class FreelancerAcceptComponent {

  @Input() jobId: string = '';
  @Input() proposal: Proposal = new Proposal();

  freelancerAcceptProposalCommand: FreelancerAcceptProposalCommand = new FreelancerAcceptProposalCommand();

  constructor(
    private proposalService: ProposalService,
    private snackBarService: SnackBarsService) { }

  ngOnInit() {
    this.freelancerAcceptProposalCommand.jobId = this.jobId;
    this.freelancerAcceptProposalCommand.proposalId = this.proposal.id;
  }

  accept() {
    this.proposalService.freelancerAccept(this.freelancerAcceptProposalCommand).subscribe(() => {
      this.snackBarService.primary('You accepted proposal successfully');
      this.proposal.status = ProposalStatus.ACCEPTED;
    });
  }

}
