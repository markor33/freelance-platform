import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CompleteRegisterDialogComponent } from '../../freelancer/complete-register-dialog/complete-register-dialog.component';
import { FreelancerService } from '../../freelancer/services/freelancer.service';
import { AuthService } from '../../auth/services/auth.service';

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
    private freelancerService: FreelancerService) { 
      this.authService.loginObserver.subscribe({
        next: (val) => this.isUserLogged = val
      });
  }

  ngOnInit() {
    this.freelancerService.freelancerObserver.subscribe((freelancer) => {
      if (freelancer != null || !this.authService.isLogged())
        return;
      this.dialog.open(CompleteRegisterDialogComponent, {
        width: '40%',
        height: '65%'
      });
    })
  }

  logout() {
    this.authService.logout();
  }

}
