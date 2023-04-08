import { Component } from '@angular/core';
import { Availability, ExperienceLevel, Freelancer } from '../models/freelancer.model';
import { FreelancerService } from '../services/freelancer.service';
import { LanguageProficiencyLevel } from '../../shared/models/language.model';

@Component({
  selector: 'app-freelancer-profile',
  templateUrl: './freelancer-profile.component.html',
  styleUrls: ['./freelancer-profile.component.scss']
})
export class FreelancerProfileComponent {

  freelancer: Freelancer = new Freelancer();

  constructor(private freelancerService: FreelancerService) { }

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
    console.log(this.freelancer);
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

}
