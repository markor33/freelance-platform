import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Certification, Education, Employment, Freelancer } from '../models/freelancer.model';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { AuthService } from '../../auth/services/auth.service';
import { AddCertificationCommand } from '../models/commands/add-certification-command.model';
import { Skill } from '../../shared/models/profession.mode';
import { AddEducationCommand } from '../models/commands/add-education-command.model';
import { AddEmploymentCommand } from '../models/commands/add-employment-command.model';
import { AddSkillCommand } from '../models/commands/add-skill-command.model';
import { CreateFreelancerCommand } from '../models/commands/create-freelancer-command.model';
import { EditCertificationCommand } from '../models/commands/edit-certification-command.model';
import { EditEducationCommand } from '../models/commands/edit-education-command.model';
import { EditEmploymentCommand } from '../models/commands/edit-employment-command.model';

@Injectable({
  providedIn: 'root'
})
export class FreelancerService {

  httpOptions = {
    headers: { 'Content-Type': 'application/json' }
  };

  currentFreelancer: Freelancer = new Freelancer();

  constructor(private httpClient: HttpClient, private authService: AuthService) { }

  completeRegistration(createCommand: CreateFreelancerCommand): Observable<any> {
    return this.httpClient.post<Freelancer>('api/freelancer/freelancer', createCommand, this.httpOptions)
    .pipe(
      map((res) => {
        this.authService.addDomainData(res.id, res.firstName, res.lastName);
      })
    );
  }

  get(freelancerId: string): Observable<Freelancer> {
    return this.httpClient.get<Freelancer>(`api/freelancer/freelancer/${freelancerId}`, this.httpOptions)
      .pipe(
        map((freelancer) => {
          this.currentFreelancer = freelancer;
          return freelancer;
        })
      );
  }

  addEducation(addEducationCommand: AddEducationCommand): Observable<void> {
    return this.httpClient.post<Education>('api/freelancer/freelancer/education', addEducationCommand, this.httpOptions)
      .pipe(
        map((education) => {
          this.currentFreelancer.educations.push(education);
        })
      );
  }

  editEducation(editEducationCommand: EditEducationCommand): Observable<void> {
    const url = `api/freelancer/freelancer/${this.currentFreelancer.id}/education/${editEducationCommand.educationId}`;
    return this.httpClient.put<Education>(url, editEducationCommand)
      .pipe(
        map((education) => {
          const index = this.currentFreelancer.educations.findIndex(c => c.id === education.id);
          if (index != -1)
            this.currentFreelancer.educations[index] = education;
        })
      );
  }

  deleteEducation(educationId: string): Observable<any> {
    return this.httpClient.delete<any>(`api/freelancer/freelancer/${this.currentFreelancer.id}/education/${educationId}`)
      .pipe(
        map(() => {
          const index = this.currentFreelancer.educations.findIndex(c => c.id === educationId);
          if (index != -1)
            this.currentFreelancer.educations.splice(index, 1);
        })
      );
  }

  addCertification(addCertificationCommand: AddCertificationCommand): Observable<void> {
    return this.httpClient.post<Certification>('api/freelancer/freelancer/certification', addCertificationCommand, this.httpOptions)
      .pipe(
        map((certification) => {
          this.currentFreelancer.certifications.push(certification);
        })
      );
  }

  editCertification(editCertificationCommand: EditCertificationCommand): Observable<void> {
    const url = `api/freelancer/freelancer/${this.currentFreelancer.id}/certification/${editCertificationCommand.certificationId}`;
    return this.httpClient.put<Certification>(url, editCertificationCommand)
      .pipe(
        map((certification) => {
          const index = this.currentFreelancer.certifications.findIndex(c => c.id === certification.id);
          if (index != -1)
            this.currentFreelancer.certifications[index] = certification;
        })
      );
  }

  deleteCertification(certificationId: string): Observable<any> {
    return this.httpClient.delete<any>(`api/freelancer/freelancer/${this.currentFreelancer.id}/certification/${certificationId}`)
      .pipe(
        map(() => {
          const index = this.currentFreelancer.certifications.findIndex(c => c.id === certificationId);
          if (index != -1)
            this.currentFreelancer.certifications.splice(index, 1);
        })
      );
  }

  addEmployment(addEmploymentCommand: AddEmploymentCommand): Observable<void> {
    return this.httpClient.post<Employment>('api/freelancer/freelancer/employment', addEmploymentCommand, this.httpOptions)
      .pipe(
        map((employment) => {
          this.currentFreelancer.employments.push(employment);
        })
      );
  }

  editEmployment(editEmploymentCommand: EditEmploymentCommand): Observable<void> {
    const url = `api/freelancer/freelancer/${this.currentFreelancer.id}/employment/${editEmploymentCommand.employmentId}`
    return this.httpClient.put<Employment>(url, editEmploymentCommand)
      .pipe(
        map((employment) => {
          const index = this.currentFreelancer.employments.findIndex(c => c.id === employment.id);
          if (index != -1)
            this.currentFreelancer.employments[index] = employment;
        })
      );
  }

  deleteEmployment(employmentId: string): Observable<void> {
    return this.httpClient.delete<any>(`api/freelancer/freelancer/${this.currentFreelancer.id}/employment/${employmentId}`)
      .pipe(
        map(() => {
          const index = this.currentFreelancer.employments.findIndex(c => c.id === employmentId);
          if (index != -1)
            this.currentFreelancer.employments.splice(index, 1);
        })
      );
  }

  addSkills(addSkillsCommand: AddSkillCommand): Observable<void> {
    return this.httpClient.post<Skill[]>('api/freelancer/freelancer/skill', addSkillsCommand, this.httpOptions)
      .pipe(
        map((skills) => {
          this.currentFreelancer.skills = skills;
        })
      );
  }

}
