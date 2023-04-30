import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CompleteRegisterDialogComponent } from '../../freelancer/complete-register-dialog/complete-register-dialog.component';
import { AuthService } from '../../auth/services/auth.service';
import { ClientCompleteRegistrationComponent } from '../../client/client-complete-registration/client-complete-registration.component';
import { Observable } from 'rxjs';
import { ComponentType } from '@angular/cdk/portal';
import { NotificationService } from '../../notification/services/notification.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  isUserLogged: boolean = false;
  userRole: string = '';
  domainUserId: string = '';

  constructor(
    private dialog: MatDialog, 
    private authService: AuthService,
    private notificationService: NotificationService) {
      this.authService.userObserver.subscribe({
        next: (user) => {
          this.isUserLogged = user !== null;
          this.userRole = user?.role as string;
          this.domainUserId = user?.domainId as string;
        }
      });
  }

  ngOnInit() {
    if (!this.authService.isLogged() || this.authService.hasDomainData())
      return;
    if (this.userRole === 'FREELANCER')
      this.dialog.open(CompleteRegisterDialogComponent, { width: '40%', height: '65%' });
    else
      this.dialog.open(ClientCompleteRegistrationComponent, { width: '40%', height: '65%' });
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
