import { Component } from '@angular/core';
import { AddEducationCommand } from '../../../models/add-education-command.model';
import { FreelancerService } from '../../../services/freelancer.service';
import { MatDialogRef } from '@angular/material/dialog';
import { SnackBarsService } from 'src/app/modules/shared/services/snack-bars.service';

@Component({
  selector: 'app-add-education-dialog',
  templateUrl: './add-education-dialog.component.html',
  styleUrls: ['./add-education-dialog.component.scss']
})
export class AddEducationDialogComponent {

  addEducationCommand: AddEducationCommand = new AddEducationCommand();
  attended = {
    start: new Date(),
    end: new Date()
  }

  constructor(
    private dialogRef: MatDialogRef<AddEducationDialogComponent>,
    private freelancerService: FreelancerService, 
    private snackBars: SnackBarsService) { }

  add() {
    this.addEducationCommand.start = this.convertToUTCDate(this.attended.start);
    this.addEducationCommand.end = this.convertToUTCDate(this.attended.end);
    this.freelancerService.addEducation(this.addEducationCommand).subscribe({
      complete: this.educationSuccessfullyAdded.bind(this),
      error: (err) => console.log(err)
    });
  }

  educationSuccessfullyAdded(): void {
    this.snackBars.primary('Education successfully added');
    this.dialogRef.close()
  }

  convertToUTCDate(date: Date) {
    const timezoneOffset = this.attended.start.getTimezoneOffset();
    const utcDate = new Date(date.getTime() - timezoneOffset * 60 * 1000);
    return utcDate;
  }

}