import { Component, Input } from '@angular/core';
import { EditProposalPaymentCommand } from 'src/app/modules/job/models/commands/edit-proposal-payment-command-model';
import { Payment } from 'src/app/modules/job/models/payment.model';
import { Proposal, ProposalStatus } from 'src/app/modules/job/models/proposal.model';
import { ProposalService } from 'src/app/modules/job/services/proposal.service';
import { SnackBarsService } from 'src/app/modules/shared/services/snack-bars.service';
import { ClientAcceptProposalCommand } from 'src/app/modules/job/models/commands/client-accept-proposal-command.model';

@Component({
  selector: 'app-client-accept',
  templateUrl: './client-accept.component.html',
  styleUrls: ['./client-accept.component.scss']
})
export class ClientAcceptComponent {

  paymentPanelOpenState: boolean = false;
  @Input() jobId: string = '';
  @Input() proposal: Proposal = new Proposal();

  payment: Payment = new Payment();
  editProposalPaymentCommand = new EditProposalPaymentCommand();
  clientAcceptProposalCommand = new ClientAcceptProposalCommand();

  constructor(
    private proposalService: ProposalService,
    private snackBarService: SnackBarsService) { }

  ngOnInit() {
    this.payment = {...this.proposal.payment};
    this.editProposalPaymentCommand.jobId = this.jobId;
    this.editProposalPaymentCommand.proposalId = this.proposal.id;
    this.editProposalPaymentCommand.payment = this.payment;
    this.clientAcceptProposalCommand.jobId = this.jobId;
    this.clientAcceptProposalCommand.proposalId = this.proposal.id;
  }

  editPayment() {
    this.proposalService.editPayment(this.editProposalPaymentCommand).subscribe(() => {
      this.snackBarService.primary('Proposals payment successfully changed');
      this.proposal.payment = this.payment;
    });
  }

  accept() {
    this.proposalService.clientAccept(this.clientAcceptProposalCommand).subscribe(() => {
      this.snackBarService.primary('You accepted proposal successfully');
      this.proposal.status = ProposalStatus.CLIENT_ACCEPTED;
    });
  }
  
}
