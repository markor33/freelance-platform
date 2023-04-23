import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FreelancerService } from '../../freelancer/services/freelancer.service';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { CreateProposalCommand } from '../models/create-proposal-cmmand.model';
import { Proposal, ProposalStatus } from '../models/proposal.model';

@Injectable({
  providedIn: 'root'
})
export class ProposalService {

  freelancerId: string = '';

  private isConfirmedSource: BehaviorSubject<boolean | null> = new BehaviorSubject<boolean | null>(null);
  public isConfirmedObserver: Observable<boolean | null> = this.isConfirmedSource.asObservable();

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(
    private httpClient: HttpClient,
    private freelancerService: FreelancerService,
    
  ) { 
    this.freelancerService.freelancerObserver.subscribe((freelancer) => {
      this.freelancerId = freelancer?.id as string;
    });
  }

  get(id: string): Observable<Proposal> {
    return this.httpClient.get<Proposal>(`api/job/job/proposal/${id}`, this.httpOptions);
  }

  create(createProposalCommand: CreateProposalCommand): Observable<Observable<boolean | null>> {
    createProposalCommand.freelancerId = this.freelancerId;
    return this.httpClient.post<Proposal>('api/job/job/proposal', createProposalCommand, this.httpOptions)
    .pipe(
      map((proposal) => {
        this.isConfirmed(proposal.id);
        return this.isConfirmedObserver;
      })
    );
  }

  isConfirmed(id: string): void {
    const intervalId = setInterval(() => {
      this.get(id).subscribe({
        next: (proposal) => {
          if (proposal.status === ProposalStatus.SENT)
            this.isConfirmedSource.next(true);
        },
        error: (err) => this.isConfirmedSource.next(false)
      });
    }, 500);

    this.isConfirmedObserver.subscribe((isConfirmed) => {
      if (isConfirmed === null)
        return;
      clearInterval(intervalId);
      this.isConfirmedSource.next(null);
    });
  }

}
