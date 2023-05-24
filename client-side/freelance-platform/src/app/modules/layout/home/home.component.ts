import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { CompleteRegisterDialogComponent } from '../../freelancer/complete-register-dialog/complete-register-dialog.component';
import { AuthService } from '../../auth/services/auth.service';
import { ClientCompleteRegistrationComponent } from '../../client/client-complete-registration/client-complete-registration.component';
import { Observable } from 'rxjs';
import { ComponentType } from '@angular/cdk/portal';
import { NotificationService } from '../../notification/services/notification.service';
import { ChatService } from '../../chat/services/chat.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  showNotifications: boolean = false;
  showNewNotificationDot: boolean = false;
  showNewChatMessageDot: boolean = false;
  isUserLogged: boolean = false;
  userRole: string = '';

  constructor(
    private dialog: MatDialog,
    private router: Router,
    private authService: AuthService,
    private notificationService: NotificationService,
    private chatService: ChatService) {
      this.authService.userObserver.subscribe({
        next: (user) => {
          this.isUserLogged = user !== null;
          this.userRole = user?.role as string;
        }
      });
      this.notificationService.newNotificationReceivedObserver.subscribe((res) => this.showNewNotificationDot = res);
      this.chatService.newMessageObserver.subscribe((res) => {
        if (res === null) this.showNewChatMessageDot = false;
        else this.showNewChatMessageDot = true;
      });
  }

  ngOnInit() {
    if (this.authService.hasDomainData()) {
      this.route();
      return;
    }
    if (this.userRole === 'FREELANCER')
      this.dialog.open(CompleteRegisterDialogComponent, { width: '40%', height: '65%' });
    else
      this.dialog.open(ClientCompleteRegistrationComponent, { width: '40%', height: '65%' });
    console.log('init');
  }

  route() {
    console.log('asdasdasd');
    if (this.userRole === 'FREELANCER')
      this.router.navigate(['job']);
    else
      this.router.navigate(['job-management']);
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

  navigateToProfile() {
    this.authService.userObserver.subscribe((user) => {
      this.router.navigate([`/freelancer/profile/${user?.domainId}`]);
    });
  }

  openNotificationsDisplay() {
    this.showNotifications = !this.showNotifications;
    this.showNewNotificationDot = false;
  }

  logout() {
    this.authService.logout();
  }

}
