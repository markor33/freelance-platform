import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { FreelancerService } from '../services/freelancer.service';
import { CreateFreelancerCommand } from '../models/create-freelancer-command.model';

@Component({
  selector: 'app-complete-register-dialog',
  templateUrl: './complete-register-dialog.component.html',
  styleUrls: ['./complete-register-dialog.component.scss']
})
export class CompleteRegisterDialogComponent {

  isRegistrationCompleted: boolean = false;
  createFreelancerCommand: CreateFreelancerCommand = new CreateFreelancerCommand();

  constructor(
    private dialogRef: MatDialogRef<CompleteRegisterDialogComponent>,
    private freelancerService: FreelancerService,
    private dialog: MatDialog) 
  {
    this.freelancerService.freelancerObserver.subscribe((freelancer) => {
      this.isRegistrationCompleted = (freelancer != null);
    });
    
    this.dialogRef.afterClosed().subscribe(() => {
      if (!this.isRegistrationCompleted)
        this.dialog.open(CompleteRegisterDialogComponent, {
          width: '40%',
          height: '60%'
        });
    });
  }

  completeRegistration(): void {
    this.freelancerService.completeRegistration(this.createFreelancerCommand).subscribe({
      complete: () => this.dialogRef.close(),
      error: () => console.log('error')
    });
  }

}
