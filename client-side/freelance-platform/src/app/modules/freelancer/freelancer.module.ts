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
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { HttpClientModule } from '@angular/common/http';
import { MatRadioModule } from '@angular/material/radio';
import { MatIconModule } from '@angular/material/icon';

const routes: Routes = [
  { path: 'complete', component: CompleteRegisterDialogComponent}
];

@NgModule({
  declarations: [
    CompleteRegisterDialogComponent
  ],
  imports: [
    HttpClientModule,
    FormsModule,
    MatInputModule,
    CommonModule,
    MatButtonModule,
    MatStepperModule,
    MatSelectModule,
    MatRadioModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatIconModule,
    ReactiveFormsModule,
    MatGridListModule,
    RouterModule.forRoot(routes)
  ]
})
export class FreelancerModule { }
