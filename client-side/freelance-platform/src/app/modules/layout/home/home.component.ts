import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CompleteRegisterDialogComponent } from '../../freelancer/complete-register-dialog/complete-register-dialog.component';
import { FreelancerService } from '../../freelancer/services/freelancer.service';
import { AuthService } from '../../auth/services/auth.service';
import { ClientCompleteRegistrationComponent } from '../../client/client-complete-registration/client-complete-registration.component';
import { ClientService } from '../../client/services/client.service';
import { Observable } from 'rxjs';
import { ComponentType } from '@angular/cdk/portal';
import { CreateJobDialogComponent } from '../../job/create-job-dialog/create-job-dialog.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  isUserLogged: boolean = false;

  constructor(
    private dialog: MatDialog, 
    private authService: AuthService,
    private freelancerService: FreelancerService,
    private clientService: ClientService) { 
      this.authService.loginObserver.subscribe({
        next: (val) => this.isUserLogged = val
      });
  }

  ngOnInit() {
    if (!this.authService.isLogged())
      return;
    const role = this.authService.getUserRole(); 
    if (role === 'FREELANCER')
      this.isRegistrationComplete(this.freelancerService.freelancerObserver, CompleteRegisterDialogComponent);
    else
      this.isRegistrationComplete(this.clientService.clientObserver, ClientCompleteRegistrationComponent);
  }

  isRegistrationComplete(profileObserver: Observable<any | null>, dialog: ComponentType<any>): void {
    profileObserver.subscribe((profile) => {
      if (profile != null)
        return;
        this.dialog.open(dialog, {
          width: '40%',
          height: '65%'
        });
    });
  }

  logout() {
    this.authService.logout();
  }

}
