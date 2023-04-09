import { Component } from '@angular/core';
import { AddEducationCommand } from '../../../models/add-education-command.model';
import { FreelancerService } from '../../../services/freelancer.service';
import * as moment from 'moment';
import { Education } from '../../../models/freelancer.model';

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

  constructor(private freelancerService: FreelancerService) { }

  add() {
    this.addEducationCommand.start = this.convertToUTCDate(this.attended.start);
    this.addEducationCommand.end = this.convertToUTCDate(this.attended.end);
    this.freelancerService.addEducation(this.addEducationCommand).subscribe({
      next: this.educationSuccessfullyAdded,
      error: (err) => console.log(err)
    });
  }

  educationSuccessfullyAdded(education: Education): void {
    console.log(education);
  }

  convertToUTCDate(date: Date) {
    const timezoneOffset = this.attended.start.getTimezoneOffset();
    const utcDate = new Date(date.getTime() - timezoneOffset * 60 * 1000);
    return utcDate;
  }

}