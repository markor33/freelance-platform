import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateJobDialogComponent } from './create-job-dialog/create-job-dialog.component';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatChipsModule} from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { HttpClientModule } from '@angular/common/http';
import { JobListingComponent } from './job-listing/job-listing.component';
import { Routes } from '@angular/router';
import { ApplyDialogComponent } from './apply-dialog/apply-dialog.component';
import {MatDividerModule} from '@angular/material/divider';

export const jobRoutes: Routes = [
  { path: 'jobs', component: JobListingComponent }
]

@NgModule({
  declarations: [
    CreateJobDialogComponent,
    JobListingComponent,
    ApplyDialogComponent
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
    MatButtonModule
  ],
  exports: [
    
  ]
})
export class JobModule { }
