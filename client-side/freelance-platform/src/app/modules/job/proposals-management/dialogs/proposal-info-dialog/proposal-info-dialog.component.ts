import { Component, Inject } from '@angular/core';
import { Proposal } from '../../../models/proposal.model';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EnumConverter } from 'src/app/modules/shared/utils/enum-string-converter.util';
import { ProposalService } from '../../../services/proposal.service';
import { ChatService } from 'src/app/modules/chat/services/chat.service';
import { SnackBarsService } from 'src/app/modules/shared/services/snack-bars.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-proposal-info-dialog',
  templateUrl: './proposal-info-dialog.component.html',
  styleUrls: ['./proposal-info-dialog.component.scss']
})
export class ProposalInfoDialogComponent {

  jobId: string = '';
  proposalId: string = '';
  proposal: Proposal = new Proposal();
  message: string = '';

  constructor(
    public enumConverter: EnumConverter,
    private chatService: ChatService,
    private snackBarService: SnackBarsService,
    private router: Router,
    private dialogRef: MatDialogRef<ProposalInfoDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {jobId: string, proposalId: string},
    private proposalService: ProposalService) {
      this.jobId = data.jobId;
      this.proposalId = data.proposalId;
  }

  ngOnInit() {
    this.proposalService.get(this.proposalId).subscribe((proposal) => this.proposal = proposal);
    // this.proposalService.getAnswers(this.proposal.id).subscribe(answers => this.proposal.answers = answers);
  }

  sendMessage() {
    this.chatService.create(this.jobId, this.proposal, this.message).subscribe(() => {
      this.snackBarService.primary('Message sent to freelancer successfully');
      this.dialogRef.close();
      this.router.navigate(['/chat']);
    });
  }


}
