import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CompleteRegisterDialogComponent } from './complete-register-dialog/complete-register-dialog.component';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { HttpClientModule } from '@angular/common/http';

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
    RouterModule.forRoot(routes)
  ]
})
export class FreelancerModule { }
