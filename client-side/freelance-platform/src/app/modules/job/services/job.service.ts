import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClientService } from '../../client/services/client.service';
import { Observable } from 'rxjs';
import { CreateJobCommand } from '../models/create-job-command.model';
import { Job } from '../models/job.model';

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
    private clientService: ClientService) { 
    this.clientService.clientObserver.subscribe((client) => {
      this.clientId = client?.id as string;
    });
  }

  create(createJobCommand: CreateJobCommand) : Observable<void> {
    createJobCommand.clientId = this.clientId;
    return this.httpClient.post<any>('api/job/job', createJobCommand, this.httpOptions);
  }

  getAll(): Observable<Job[]> {
    return this.httpClient.get<Job[]>('api/job/job');
  }
  
}
