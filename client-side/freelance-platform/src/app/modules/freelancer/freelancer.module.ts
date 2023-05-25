import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CompleteRegisterDialogComponent } from './complete-register-dialog/complete-register-dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import {MatStepperModule} from '@angular/material/stepper';
import {MatSelectModule} from '@angular/material/select';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { HttpClientModule } from '@angular/common/http';
import { MatRadioModule } from '@angular/material/radio';
import { MatIconModule } from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import { FreelancerProfileComponent } from './freelancer-profile/freelancer-profile.component';
import { MatCardModule } from '@angular/material/card';
import { AddEducationDialogComponent } from './freelancer-profile/dialogs/add-education-dialog/add-education-dialog.component';
import { MatNativeDateModule, MatOptionModule } from '@angular/material/core';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import {MatChipsModule} from '@angular/material/chips';
import { SharedModule } from '../shared/shared.module';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { AddCertificationDialogComponent } from './freelancer-profile/dialogs/certification/add-certification-dialog/add-certification-dialog.component';
import { AddEmploymentDialogComponent } from './freelancer-profile/dialogs/add-employment-dialog/add-employment-dialog.component';
import { AddSkillDialogComponent } from './freelancer-profile/dialogs/add-skill-dialog/add-skill-dialog.component';
import { EditCertificationDialogComponent } from './freelancer-profile/dialogs/certification/edit-certification-dialog/edit-certification-dialog.component';

export const freelancerRoutes: Routes = [
  { path: 'freelancer/profile/:id', component: FreelancerProfileComponent}
];

@NgModule({
  declarations: [
    CompleteRegisterDialogComponent,
    FreelancerProfileComponent,
    AddEducationDialogComponent,
    AddCertificationDialogComponent,
    AddEmploymentDialogComponent,
    AddSkillDialogComponent,
    EditCertificationDialogComponent
  ],
  imports: [
    HttpClientModule,
    FormsModule,
    MatInputModule,
    CommonModule,
    SharedModule,
    MatButtonModule,
    MatStepperModule,
    MatSelectModule,
    MatOptionModule,
    MatChipsModule,
    MatAutocompleteModule,
    MatRadioModule,
    MatDatepickerModule,
    MatDividerModule,
    MatSnackBarModule,
    MatNativeDateModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatIconModule,
    ReactiveFormsModule,
    MatGridListModule
  ]
})
export class FreelancerModule { }
