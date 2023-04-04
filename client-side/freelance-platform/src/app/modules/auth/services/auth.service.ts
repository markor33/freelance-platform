import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Credentials } from '../models/credentials.model';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { Registration } from '../models/registration.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private userClaims: any = null;
  private loginSource = new BehaviorSubject<boolean>(false);
  public loginObserver = this.loginSource.asObservable();

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(private httpClient: HttpClient, private jwtHelper: JwtHelperService) {
    this.userClaims = this.jwtHelper.decodeToken();
    if(this.userClaims)
      this.loginSource.next(true);
  }

  login(credentials: Credentials): Observable<any> {
    const body = new HttpParams()
      .set('username', credentials.username)
      .set('password', credentials.password)
      .set('grant_type', 'password')
      .set('client_id', 'angular-app')
      .set('client_secret', 'secret');

    return this.httpClient.post<any>('api/identity/connect/token', body)
      .pipe(
        map((res) => {
          localStorage.setItem('token', res.access_token);
          this.userClaims = this.jwtHelper.decodeToken();
          this.loginSource.next(true);
        })
      );
  }

  register(registration: Registration): Observable<any> {
    return this.httpClient.post<any>('api/identity/api/auth/register', registration, this.httpOptions);
  }

  logout(): void {
    localStorage.clear();
    this.loginSource.next(false);
  }

  getUserRole(): string {
    return this.userClaims.role;
  }

  isLogged(): boolean {
    return this.loginSource.value;
  }

}
