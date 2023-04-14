import { Component } from '@angular/core';
import { AddEmploymentCommand } from '../../../models/add-employment-command.model';
import { FreelancerService } from '../../../services/freelancer.service';
import { SnackBarsService } from 'src/app/modules/shared/services/snack-bars.service';
import { MatDialogRef } from '@angular/material/dialog';
import { convertToUTCDate } from 'src/app/modules/shared/utils/date-helper.util';

@Component({
  selector: 'app-add-employment-dialog',
  templateUrl: './add-employment-dialog.component.html',
  styleUrls: ['./add-employment-dialog.component.scss']
})
export class AddEmploymentDialogComponent {

  addEmploymentCommand: AddEmploymentCommand = new AddEmploymentCommand();
  period = {
    start: new Date(),
    end: new Date()
  }

  constructor(
    private dialogRef: MatDialogRef<AddEmploymentDialogComponent>,
    private freelancerServie: FreelancerService,
    private snackBars: SnackBarsService) { }

  add(): void {
    this.addEmploymentCommand.start = convertToUTCDate(this.period.start);
    this.addEmploymentCommand.end = convertToUTCDate(this.period.end);
    this.freelancerServie.addEmployment(this.addEmploymentCommand).subscribe({
      complete: this.employmentSuccessfullyAdded.bind(this)
    });
  }

  employmentSuccessfullyAdded() {
    this.snackBars.primary('Employment successfully added');
    this.dialogRef.close();
  }

}
