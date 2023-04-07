import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Freelancer } from '../models/freelancer.model';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { AuthService } from '../../auth/services/auth.service';
import { CreateFreelancerCommand } from '../models/create-freelancer-command.model';

@Injectable({
  providedIn: 'root'
})
export class FreelancerService {

  private freelancerSource: BehaviorSubject<Freelancer | null> = new BehaviorSubject<Freelancer | null>(new Freelancer());
  public freelancerObserver: Observable<Freelancer | null> = this.freelancerSource.asObservable();

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
      this.freelancerSource.next(new Freelancer());
      return;
    }
    this.httpClient.get<Freelancer>('api/freelancer/freelancer', this.httpOptions).subscribe({
      next: (freelancer) => this.freelancerSource.next(freelancer),
      error: (err) => this.freelancerSource.next(null)
    });
  }

  completeRegistration(createCommand: CreateFreelancerCommand): Observable<any> {
    return this.httpClient.post<any>('api/freelancer/freelancer', createCommand, this.httpOptions)
    .pipe(
      map((res) => {
        this.freelancerSource.next(new Freelancer())
      })
    );
  }

}
