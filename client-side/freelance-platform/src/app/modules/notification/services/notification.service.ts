import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { ClientService } from '../../client/services/client.service';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(
    private httpClient: HttpClient) {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(`hub/notifications?domainUserId=98a13b27-cb05-4dbb-89e9-f356d4f4e689`)
      .build();

    connection.start()
      .then(() => console.log('Ok'))
      .catch((err) => console.log(err));

    connection.on('ProposalSubmittedNotification', (msg) => {
      console.log(msg);
    });
  }

}
