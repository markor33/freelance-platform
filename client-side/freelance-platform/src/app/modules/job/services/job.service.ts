import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateJobCommand } from '../models/commands/create-job-command.model';
import { Job } from '../models/job.model';
import { AuthService } from '../../auth/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class JobService {

  clientId: string = '';

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(
    private httpClient: HttpClient, 
    private authService: AuthService) { 
    this.authService.userObserver.subscribe((user) => {
      this.clientId = user?.domainId as string;
    });
  }

  create(createJobCommand: CreateJobCommand) : Observable<Job> {
    createJobCommand.clientId = this.clientId;
    return this.httpClient.post<Job>('api/job/job', createJobCommand, this.httpOptions);
  }

  getAll(): Observable<Job[]> {
    return this.httpClient.get<Job[]>('api/job/job');
  }

  get(id: string): Observable<Job> {
    return this.httpClient.get<Job>(`api/job/job/${id}`);
  }

  getByClient(): Observable<Job[]> {
    return this.httpClient.get<Job[]>(`api/job/job/client/${this.clientId}`, this.httpOptions);
  }

  delete(id: string): Observable<void> {
    return this.httpClient.delete<any>(`api/job/job/${id}`, this.httpOptions);
  }
  
}
