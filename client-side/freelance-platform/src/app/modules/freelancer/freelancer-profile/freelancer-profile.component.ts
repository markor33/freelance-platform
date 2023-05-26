import { Component } from '@angular/core';
import { Availability, Certification, Education, Freelancer } from '../models/freelancer.model';
import { FreelancerService } from '../services/freelancer.service';
import { LanguageProficiencyLevel } from '../../shared/models/language.model';
import { MatDialog } from '@angular/material/dialog';
import { AddEducationDialogComponent } from './dialogs/education/add-education-dialog/add-education-dialog.component';
import { AddCertificationDialogComponent } from './dialogs/certification/add-certification-dialog/add-certification-dialog.component';
import { AddEmploymentDialogComponent } from './dialogs/add-employment-dialog/add-employment-dialog.component';
import { AddSkillDialogComponent } from './dialogs/add-skill-dialog/add-skill-dialog.component';
import { ExperienceLevel } from '../../shared/models/experience-level.model';
import { AuthService } from '../../auth/services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { Feedback } from '../../feedback/models/feedback.model';
import { FeedbackService } from '../../feedback/services/feedback.service';
import { JobInfoDialogComponent } from '../../job/jobs-management/dialogs/job-info-dialog/job-info-dialog.component';
import { SnackBarsService } from '../../shared/services/snack-bars.service';
import { ConfirmationDialogComponent } from '../../shared/dialogs/confirmation-dialog/confirmation-dialog.component';
import { EditCertificationDialogComponent } from './dialogs/certification/edit-certification-dialog/edit-certification-dialog.component';
import { EditEducationDialogComponent } from './dialogs/education/edit-education-dialog/edit-education-dialog.component';

@Component({
  selector: 'app-freelancer-profile',
  templateUrl: './freelancer-profile.component.html',
  styleUrls: ['./freelancer-profile.component.scss']
})
export class FreelancerProfileComponent {

  educationHover: boolean = false;
  certificationHover: boolean = false;

  role: string = '';
  freelancer: Freelancer = new Freelancer();
  feedbacks: Feedback[] = [];
  freelancerId: string = '';

  constructor(
    private freelancerService: FreelancerService,
    private dialog: MatDialog,
    private authService: AuthService,
    private feedbackService: FeedbackService,
    private route: ActivatedRoute,
    private snackBarService: SnackBarsService) {
      const id = this.route.snapshot.paramMap.get('id');
      if(id)
        this.freelancerId = id;
      this.role = this.authService.getUserRole();
    }

  ngOnInit() {
    this.freelancerService.get(this.freelancerId).subscribe({
      next: (freelancer) => this.freelancer = freelancer
    });
    this.feedbackService.getByFreelancer(this.freelancerId).subscribe((feedbacks) => this.feedbacks = feedbacks);
  }
  
  availabilityToString(availability: Availability): string {
    if (availability == Availability.FULL_TIME)
      return 'Full time';
    return 'Part time';
  }

  experienceLevelToString(experienceLevel: ExperienceLevel): string {
    if (experienceLevel == ExperienceLevel.JUNIOR)
      return 'Junior';
    else if (experienceLevel == ExperienceLevel.MEDIOR)
      return 'Medior';
    return 'Senior';
  }

  languageProficiencyLevelToString(languageProficiencyLevel: LanguageProficiencyLevel): string {
    if (languageProficiencyLevel == LanguageProficiencyLevel.BASIC)
      return 'Basic';
    else if (languageProficiencyLevel == LanguageProficiencyLevel.CONVERSATIONAL)
      return 'Conversational';
    else if (languageProficiencyLevel == LanguageProficiencyLevel.FLUENT)
      return 'Fluent';
    return 'Native';
  }

  openAddEducationDialog() {
    AddEducationDialogComponent.open(this.dialog);
  }

  openEditEducationDialog(education: Education) {
    EditEducationDialogComponent.open(this.dialog, education);
  }

  deleteEducation(educationId: string) {
    const confirmDialog = ConfirmationDialogComponent.open(this.dialog, 'You are about to delete your education.');
    confirmDialog.afterClosed().subscribe((res) => {
      if (!res)
        return;
      this.freelancerService.deleteEducation(educationId).subscribe({
        complete: () => this.snackBarService.primary('Education deleted successfully')
      });
    }); 
  }

  openAddCertificationDialog() {
    AddCertificationDialogComponent.open(this.dialog);
  }

  openEditCertificationDialog(certification: Certification) {
    EditCertificationDialogComponent.open(this.dialog, certification);
  }

  deleteCertification(certificationId: string) {
    const confirmDialog = ConfirmationDialogComponent.open(this.dialog, 'You are about to delete your certification.');
    confirmDialog.afterClosed().subscribe((res) => {
      if (!res)
        return;
      this.freelancerService.deleteCertification(certificationId).subscribe({
        complete: () => this.snackBarService.primary('Certification deleted successfully')
      });
    }); 
  }

  openAddEmploymentDialog() {
    this.dialog.open(AddEmploymentDialogComponent, {
      width: '40%',
      height: '72%'
    });
  }

  openAddSkillDialog() {
    this.dialog.open(AddSkillDialogComponent, {
      width: '40%',
      height: '72%'
    });
  }

  openJobInfoDialog(jobId: string) {
    JobInfoDialogComponent.open(this.dialog, jobId);
  }

  getRange(value: number): number[] {
    return Array(Math.floor(value)).fill(0).map((x, i) => i);
  }

}
