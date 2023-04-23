import { Component, Inject } from '@angular/core';
import { Job } from '../models/job.model';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EnumConverter } from '../../shared/utils/enum-string-converter.util';
import { CreateProposalCommand } from '../models/create-proposal-cmmand.model';
import { ProposalService } from '../services/proposal.service';
import { SnackBarsService } from '../../shared/services/snack-bars.service';

@Component({
  selector: 'app-apply-dialog',
  templateUrl: './apply-dialog.component.html',
  styleUrls: ['./apply-dialog.component.scss']
})
export class ApplyDialogComponent {

  paymentPanelOpenState: boolean = true;
  enumUtils: EnumConverter = new EnumConverter();

  job: Job = new Job();
  createProposalCommand = new CreateProposalCommand();

  questionsAndAnswers = [] as any;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { job: Job },
    private dialogRef: MatDialogRef<ApplyDialogComponent>,
    private proposalService: ProposalService,
    private snackBars: SnackBarsService) {
    this.job = data.job;
    this.createProposalCommand.payment = { ...this.job.payment };
    this.createProposalCommand.jobId = this.job.id;
    this.setQuestionsInputs();
  }

  setQuestionsInputs() {
    for (const question of this.job.questions) {
      this.questionsAndAnswers.push({ question: question, answer: ''});
    }
  }

  apply() {
    for (const answer of this.questionsAndAnswers)
      this.createProposalCommand.answers.push({ id: '', questionId: answer.question.id, text: answer.answer});
    this.proposalService.create(this.createProposalCommand).subscribe({
      next: (isConfirmedObserver) => isConfirmedObserver.subscribe({ next: this.proposalStatusChanged.bind(this)}),
      error: (err) => this.snackBars.error('Error')
    });
  }

  proposalStatusChanged(isConfirmed: boolean | null) {
    if (isConfirmed) {
      this.snackBars.primary('Proposal created successfully');
      this.dialogRef.close();
    }
    else if (isConfirmed === false)
      this.snackBars.error("You don't have enough credits");
  }

}
