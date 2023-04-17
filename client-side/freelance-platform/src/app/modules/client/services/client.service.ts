import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Client } from '../models/client.model';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { AuthService } from '../../auth/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  private clientSource: BehaviorSubject<Client | null> = new BehaviorSubject<Client | null>(new Client());
  public clientObserver: Observable<Client | null> = this.clientSource.asObservable();

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(private httpClient: HttpClient, private authService: AuthService) { 
    this.authService.loginObserver.subscribe({
      next: this.loginStateChanged.bind(this),
    });
  }

  loginStateChanged(isLogged: boolean) {
    if (!isLogged) {
      this.clientSource.next(new Client());
      return;
    }
    this.httpClient.get<Client>('api/client/client', this.httpOptions).subscribe({
      next: (client) => {
        this.clientSource.next(client)
      },
      error: (err) => this.clientSource.next(null)
    });
  }

  completeRegistration(client: Client): Observable<void> {
    return this.httpClient.post<Client>('api/client/client', client, this.httpOptions)
      .pipe(
        map((res) => {
          this.clientSource.next(res);
        })
      );
  }
  
}
