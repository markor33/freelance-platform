import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Client } from '../models/client.model';
import { Observable, map } from 'rxjs';
import { AuthService } from '../../auth/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(private httpClient: HttpClient, private authService: AuthService) { }

  completeRegistration(client: Client): Observable<void> {
    return this.httpClient.post<Client>('api/client/client', client, this.httpOptions)
      .pipe(
        map((res) => {
          this.authService.addDomainData(res.id, res.firstName, res.lastName);
        })
      );
  }
  
}
