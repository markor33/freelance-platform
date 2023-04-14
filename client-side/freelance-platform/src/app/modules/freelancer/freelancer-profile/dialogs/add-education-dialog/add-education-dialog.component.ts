import { Component } from '@angular/core';
import { AddEducationCommand } from '../../../models/add-education-command.model';
import { FreelancerService } from '../../../services/freelancer.service';
import { MatDialogRef } from '@angular/material/dialog';
import { SnackBarsService } from 'src/app/modules/shared/services/snack-bars.service';
import { convertToUTCDate } from 'src/app/modules/shared/utils/date-helper.util';

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
    this.addEducationCommand.start = convertToUTCDate(this.attended.start);
    this.addEducationCommand.end = convertToUTCDate(this.attended.end);
    this.freelancerService.addEducation(this.addEducationCommand).subscribe({
      complete: this.educationSuccessfullyAdded.bind(this),
      error: (err) => console.log(err)
    });
  }

  educationSuccessfullyAdded(): void {
    this.snackBars.primary('Education successfully added');
    this.dialogRef.close()
  }

}