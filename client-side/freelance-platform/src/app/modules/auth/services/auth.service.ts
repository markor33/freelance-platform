import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Credentials } from '../models/credentials.model';
import { Observable, catchError, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(private httpClient: HttpClient) { }

  login(credentials: Credentials): Observable<any> {
    const body = new HttpParams()
      .set('username', credentials.username)
      .set('password', credentials.password)
      .set('grant_type', 'password')
      .set('client_id', 'angular-app')
      .set('client_secret', 'secret');

      return this.httpClient.post<any>('http://localhost:50000/connect/token', body)
        .pipe(
          map((res) => {
            localStorage.setItem('token', res.access_token);
          })
        );
  }

}
