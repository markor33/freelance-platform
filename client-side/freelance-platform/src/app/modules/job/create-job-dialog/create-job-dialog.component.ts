import { Component } from '@angular/core';
import { CreateJobCommand } from '../models/create-job-command.model';
import { Question } from '../models/question.model';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { MatChipEditedEvent, MatChipInputEvent } from '@angular/material/chips';
import { JobService } from '../services/job.service';
import { MatDialogRef } from '@angular/material/dialog';
import { SnackBarsService } from '../../shared/services/snack-bars.service';

@Component({
  selector: 'app-create-job-dialog',
  templateUrl: './create-job-dialog.component.html',
  styleUrls: ['./create-job-dialog.component.scss']
})
export class CreateJobDialogComponent {

  addOnBlur = true;
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
  paymentPanelOpenState = false;

  createJobCommand = new CreateJobCommand();

  constructor(
    private dialogRef: MatDialogRef<CreateJobDialogComponent>,
    private jobService: JobService,
    private snackBars: SnackBarsService) { }

  create() {
    this.jobService.create(this.createJobCommand).subscribe({
      complete: this.jobSuccessfullyAdded.bind(this)
    });
  }

  jobSuccessfullyAdded() {
    this.snackBars.primary('Job successfully added');
    this.dialogRef.close()
  }

  add(event: MatChipInputEvent) {
    const value = (event.value || '').trim();
    if (value !== '')
      this.createJobCommand.questions.push(new Question(value));
    event.chipInput!.clear();
  }

  remove(question: Question) {
    const index = this.createJobCommand.questions.indexOf(question);

    if (index >= 0)
      this.createJobCommand.questions.splice(index, 1);
  }

  edit(question: Question, event: MatChipEditedEvent) {
    const value = event.value.trim();

    if (!value) {
      this.remove(question);
      return;
    }

    const index = this.createJobCommand.questions.indexOf(question);
    if (index >= 0) {
      this.createJobCommand.questions[index].text = value;
    }
  }

}
