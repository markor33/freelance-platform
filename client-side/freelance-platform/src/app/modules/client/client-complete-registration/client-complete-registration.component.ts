import { Component } from '@angular/core';
import { ClientService } from '../services/client.service';
import { Client } from '../models/client.model';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-client-complete-registration',
  templateUrl: './client-complete-registration.component.html',
  styleUrls: ['./client-complete-registration.component.scss']
})
export class ClientCompleteRegistrationComponent {

  client: Client = new Client();

  constructor(
    private dialogRef: MatDialogRef<ClientCompleteRegistrationComponent>,
    private clientService: ClientService,
    private dialog: MatDialog,
    private authService: AuthService) 
    { 
      this.dialogRef.afterClosed().subscribe(() => {
        if (!this.authService.hasDomainData())
          this.dialog.open(ClientCompleteRegistrationComponent, {
            width: '40%',
            height: '60%'
          });
      });
    }

  complete(): void {
    this.client.contact.timeZoneId = 'Central Europe Standard Time';
    this.clientService.completeRegistration(this.client).subscribe({
      complete: () => this.dialogRef.close(),
      error: () => console.log('error')
    });
  }



}
