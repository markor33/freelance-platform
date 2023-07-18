import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Credentials } from '../models/credentials.model';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { Registration } from '../models/registration.model';
import { User } from '../models/user.model';
import { Router } from '@angular/router';
import { FreelancerService } from '../../freelancer/services/freelancer.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private userSource = new BehaviorSubject<User | null>(null);
  public userObserver = this.userSource.asObservable();

  constructor(
    private freelancerService: FreelancerService,
    private httpClient: HttpClient, 
    private jwtHelper: JwtHelperService,
    private router: Router) {
      const jwt = this.jwtHelper.tokenGetter();
      if (jwt === null)
        return;
      const userClaims = this.jwtHelper.decodeToken();
      this.userSource.next(new User(userClaims));
  }

  login(credentials: Credentials): Observable<any> {
    const body = new URLSearchParams();
    body.set('username', credentials.username);
    body.set('password', credentials.password);
    body.set('grant_type', 'password');
    body.set('client_id', 'angular-app');
    body.set('client_secret', 'secret');
    return this.httpClient.post<any>('api/identity/connect/token', body.toString(), { headers: { 'Content-Type': 'application/x-www-form-urlencoded' }})
      .pipe(
        map((res) => {
          const jwt = res.access_token
          this.setupJWT(jwt);
        })
      );
  }

  private setupJWT(jwt: string): void {
    localStorage.setItem('jwt', JSON.stringify(jwt));
    const userClaims = this.jwtHelper.decodeToken();
    const user = new User(userClaims);
    this.userSource.next(user);
  }

  register(registration: Registration): Observable<any> {
    return this.httpClient.post<any>('api/identity/api/auth/register', registration);
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

}
