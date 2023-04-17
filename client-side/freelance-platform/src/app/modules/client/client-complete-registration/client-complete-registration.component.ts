import { Component } from '@angular/core';
import { ClientService } from '../services/client.service';
import { Client } from '../models/client.model';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-client-complete-registration',
  templateUrl: './client-complete-registration.component.html',
  styleUrls: ['./client-complete-registration.component.scss']
})
export class ClientCompleteRegistrationComponent {

  client: Client = new Client();
  isRegistrationCompleted: boolean = false;

  constructor(
    private dialogRef: MatDialogRef<ClientCompleteRegistrationComponent>,
    private clientService: ClientService,
    private dialog: MatDialog) 
    { 
      this.clientService.clientObserver.subscribe((client) => {
        this.isRegistrationCompleted = (client != null);
      });
      
      this.dialogRef.afterClosed().subscribe(() => {
        if (!this.isRegistrationCompleted)
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
