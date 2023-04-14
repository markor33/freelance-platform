import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Skill } from '../models/profession.mode';

@Injectable({
  providedIn: 'root'
})
export class ProfessionService {

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  constructor(private httpClient: HttpClient) { }

  getSkills(professionId: string): Observable<Skill[]> {
    return this.httpClient.get<Skill[]>(`api/freelancer/profession/${professionId}/skills`);
  }

}
