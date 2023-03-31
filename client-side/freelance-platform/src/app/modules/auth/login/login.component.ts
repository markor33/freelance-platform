import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Credentials } from '../models/credentials.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  credentials: Credentials = new Credentials();

  constructor(private authService: AuthService) { }

  login() {
    this.authService.login(this.credentials).subscribe((res) => {

    },
    (err) => console.log(err));
  }

}
