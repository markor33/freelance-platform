import { Component } from '@angular/core';
import { Availability, ExperienceLevel, Freelancer } from '../models/freelancer.model';
import { FreelancerService } from '../services/freelancer.service';
import { LanguageProficiencyLevel } from '../../shared/models/language.model';
import { MatDialog } from '@angular/material/dialog';
import { AddEducationDialogComponent } from './dialogs/add-education-dialog/add-education-dialog.component';
import { AddCertificationDialogComponent } from './dialogs/add-certification-dialog/add-certification-dialog.component';
import { AddEmploymentDialogComponent } from './dialogs/add-employment-dialog/add-employment-dialog.component';
import { AddSkillDialogComponent } from './dialogs/add-skill-dialog/add-skill-dialog.component';

@Component({
  selector: 'app-freelancer-profile',
  templateUrl: './freelancer-profile.component.html',
  styleUrls: ['./freelancer-profile.component.scss']
})
export class FreelancerProfileComponent {

  freelancer: Freelancer = new Freelancer();

  constructor(
    private freelancerService: FreelancerService,
    private dialog: MatDialog) { }

  ngOnInit() {
    this.freelancerService.freelancerObserver.subscribe({
      next: (fr) => this.freelancer = fr as Freelancer
    });
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
    this.dialog.open(AddEducationDialogComponent, {
      width: '40%',
      height: '47%'
    })
  }

  openAddCertificationDialog() {
    console.log(this.freelancer);
    this.dialog.open(AddCertificationDialogComponent, {
      width: '40%',
      height: '72%'
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

}
