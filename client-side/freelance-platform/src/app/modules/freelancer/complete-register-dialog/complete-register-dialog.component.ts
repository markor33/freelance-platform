import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { FreelancerService } from '../services/freelancer.service';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ExperienceLevel, HourlyRate, ProfileSummary } from '../models/freelancer.model';
import { Language, LanguageKnowledge } from '../../shared/models/language.model';
import { Profession } from '../../shared/models/profession.mode';
import { CreateFreelancerCommand } from '../models/create-freelancer-command.model';

@Component({
  selector: 'app-complete-register-dialog',
  templateUrl: './complete-register-dialog.component.html',
  styleUrls: ['./complete-register-dialog.component.scss'],
  providers: [
    {
      provide: STEPPER_GLOBAL_OPTIONS,
      useValue: { displayDefaultIndicatorType: false, showError: true},
    },
  ],
})
export class CompleteRegisterDialogComponent {

  isRegistrationCompleted: boolean = false;

  languages: Language[] = [
    {id: 1, name: 'English', 'shortName': 'en'}, 
    {id: 2, name: 'Serbian', 'shortName': 'sr'}, 
    {id: 3, name: 'German', 'shortName': 'de'}];
  professions: Profession[] = [
    {id: '11409707-7807-4e21-9c49-04b70e34c5e8', name: 'Graphic designer'},
    {id: '14c01c08-bc78-481e-b47d-b7354d8a6362', name: 'Software engineer'}];

  generalFormGroup = this.formBuilder.group({
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    isProfilePublic: new FormControl(true, Validators.required),
    availability: new FormControl(0, Validators.required),
    contact: new FormGroup({
      address: new FormGroup({
        country: new FormControl('', Validators.required),
        city: new FormControl('', Validators.required),
        street: new FormControl('', Validators.required),
        number: new FormControl('', Validators.required),
        zipCode: new FormControl('', Validators.required),
      }),
      phoneNumber: new FormControl('', Validators.required),
    }),
  });

  professionGroup = this.formBuilder.group({
    profession: [this.professions[0], Validators.required],
    experienceLevel: [0, Validators.required]
  });

  profileSummaryGroup = this.formBuilder.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
  });

  languageKnowledgeGroup = this.formBuilder.group({
    language: [this.languages[0], Validators.required],
    profiencyLevel: [0, Validators.required]
  });

  hourlyRateGroup = this.formBuilder.group({
    amount: [0.0, Validators.required],
    currency: ['EUR', Validators.required]
  });

  availabilityGroup = this.formBuilder.group({
    availability: [0, Validators.required],
    profileType: [false, Validators.required]
  });

  constructor(
    private dialogRef: MatDialogRef<CompleteRegisterDialogComponent>,
    private freelancerService: FreelancerService,
    private dialog: MatDialog,
    private formBuilder: FormBuilder)
  {
    this.freelancerService.freelancerObserver.subscribe((freelancer) => {
      this.isRegistrationCompleted = (freelancer != null);
    });
    
    this.dialogRef.afterClosed().subscribe(() => {
      if (!this.isRegistrationCompleted)
        this.dialog.open(CompleteRegisterDialogComponent, {
          width: '40%',
          height: '65%'
        });
    });
  }

  parseToCommandModel(): CreateFreelancerCommand {
    var command = this.generalFormGroup.value as CreateFreelancerCommand;
    command.hourlyRate = this.hourlyRateGroup.value as HourlyRate;
    command.profileSummary = this.profileSummaryGroup.value as ProfileSummary;
    let languageKnowledge = this.languageKnowledgeGroup.value as LanguageKnowledge;
    command.languageId = languageKnowledge.language.id;
    command.languageProficiencyLevel = languageKnowledge.profiencyLevel;
    command.professionId = (this.professionGroup.value.profession as Profession).id;
    command.experienceLevel =  this.professionGroup.value.experienceLevel as ExperienceLevel;
    command.contact.timeZoneId = 'Central Europe Standard Time'; // HARDCODED
    return command;
  }

  completeRegistration(): void {
    var createCommand = this.parseToCommandModel();
    this.freelancerService.completeRegistration(createCommand).subscribe({
      complete: () => this.dialogRef.close(),
      error: () => console.log('error')
    });
  }

}