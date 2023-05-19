import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Credentials } from '../models/credentials.model';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { Registration } from '../models/registration.model';
import { User } from '../models/user.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private userSource = new BehaviorSubject<User | null>(null);
  public userObserver = this.userSource.asObservable();

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(
    private httpClient: HttpClient, 
    private jwtHelper: JwtHelperService,
    private router: Router) {
    const userData = JSON.parse(localStorage.getItem('user') as string);
    if (userData === null)
      return;
    const userClaims = this.jwtHelper.decodeToken(userData.jwt);
    if(userData)
      this.userSource.next(new User(userClaims.sub, userData.domainUserId, userData.firstName, userData.lastName, userClaims.role));
  }

  login(credentials: Credentials): Observable<any> {

    return this.httpClient.post<any>('api/aggregator/user/login', credentials)
      .pipe(
        map((res) => {
          localStorage.setItem('user', JSON.stringify(res));
          const userClaims = this.jwtHelper.decodeToken(res.jwt);
          const user = new User(userClaims.sub, res.domainUserId, res.firstName, res.lastName, userClaims.role);
          this.userSource.next(user);
        })
      );
  }

  register(registration: Registration): Observable<any> {
    return this.httpClient.post<any>('api/identity/api/auth/register', registration, this.httpOptions);
  }

  logout(): void {
    localStorage.clear();
    this.userSource.next(null);
    this.router.navigate(['/auth/login']);
  }

  getUserRole(): string {
    return this.userSource.value?.role as string;
  }

  isLogged(): boolean {
    return this.userSource.value !== null;
  }

  hasDomainData(): boolean {
    return !(this.userSource.value?.domainId === '00000000-0000-0000-0000-000000000000');
  }

  addDomainData(domainUserId: string, firstName: string, lastName: string): void {
    this.userSource.value?.addDomainData(domainUserId, firstName, lastName);
  }

}
