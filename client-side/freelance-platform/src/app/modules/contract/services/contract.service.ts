import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ContractService {

  constructor(private httpClient: HttpClient) { }

  create(jobId: string, proposalId: string): Observable<any> {
    return this.httpClient.post<any>(`api/job/job/${jobId}/contract/proposal/${proposalId}`, {});
  }

}
