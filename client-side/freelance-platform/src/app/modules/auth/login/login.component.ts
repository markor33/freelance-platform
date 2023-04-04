import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Credentials } from '../models/credentials.model';
import { SnackBarsService } from '../../shared/services/snack-bars.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  hidePassword = true;
  credentials: Credentials = new Credentials();

  constructor(
    private router: Router,
    private authService: AuthService,
    private snackBars: SnackBarsService) { }

  login(): void {
    this.authService.login(this.credentials).subscribe({
      complete: this.loginSuccessful.bind(this),
      error: this.loginError.bind(this)
    });
  }

  loginSuccessful(): void {
    this.router.navigate(['/']);
  }

  loginError(err: any): void {
    this.snackBars.error('Username and/or password do not match');
  }

}
