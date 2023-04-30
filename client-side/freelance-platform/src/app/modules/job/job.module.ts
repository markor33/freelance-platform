import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateJobDialogComponent } from './create-job-dialog/create-job-dialog.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatChipsModule} from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { HttpClientModule } from '@angular/common/http';
import { Routes } from '@angular/router';
import { ApplyDialogComponent } from './apply-dialog/apply-dialog.component';
import {MatDividerModule} from '@angular/material/divider';
import { JobSearchComponent } from './job-search/job-search.component';
import { JobsManagementComponent } from './jobs-management/jobs-management.component';
import {MatTableModule} from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import {MatPaginatorModule} from '@angular/material/paginator';
import { JobInfoDialogComponent } from './jobs-management/dialogs/job-info-dialog/job-info-dialog.component';

export const jobRoutes: Routes = [
  { path: 'job', component: JobSearchComponent },
  { path: 'job-management', component: JobsManagementComponent }
]

@NgModule({
  declarations: [
    CreateJobDialogComponent,
    JobSearchComponent,
    ApplyDialogComponent,
    JobsManagementComponent,
    JobInfoDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    MatInputModule,
    MatSelectModule,
    MatExpansionModule,
    MatChipsModule,
    MatDividerModule,
    MatIconModule,
    HttpClientModule,
    MatDialogModule,
    MatTableModule,
    MatPaginatorModule,
    ReactiveFormsModule,
    MatButtonModule
  ],
  exports: [
    
  ]
})
export class JobModule { }
