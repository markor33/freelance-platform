import { Component } from '@angular/core';
import { AddCertificationCommand } from '../../../models/add-certification-command.model';
import { MatDialogRef } from '@angular/material/dialog';
import { FreelancerService } from '../../../services/freelancer.service';
import { SnackBarsService } from 'src/app/modules/shared/services/snack-bars.service';

@Component({
  selector: 'app-add-certification-dialog',
  templateUrl: './add-certification-dialog.component.html',
  styleUrls: ['./add-certification-dialog.component.scss']
})
export class AddCertificationDialogComponent {

  addCertificationCommand: AddCertificationCommand = new AddCertificationCommand();
  attended = {
    start: new Date(),
    end: new Date()
  }

  constructor(
    private dialogRef: MatDialogRef<AddCertificationDialogComponent>,
    private freelancerService: FreelancerService,
    private snackBars: SnackBarsService) {}

  add() {
    this.addCertificationCommand.start = this.convertToUTCDate(this.attended.start);
    this.addCertificationCommand.end = this.convertToUTCDate(this.attended.end);
    this.freelancerService.addCertification(this.addCertificationCommand).subscribe({
      complete: this.certificationSuccessfullyAdded.bind(this)
    });
  }

  certificationSuccessfullyAdded() {
    this.snackBars.primary('Certification successfully added');
    this.dialogRef.close()
  }

  convertToUTCDate(date: Date) {
    const timezoneOffset = this.attended.start.getTimezoneOffset();
    const utcDate = new Date(date.getTime() - timezoneOffset * 60 * 1000);
    return utcDate;
  }

}
