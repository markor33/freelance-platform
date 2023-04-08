import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { MatDialogModule } from '@angular/material/dialog';
import {MatToolbarModule} from '@angular/material/toolbar';
import { FreelancerModule, freelancerRoutes } from '../freelancer/freelancer.module';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule, Routes } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';

const routes: Routes = [
  { path: '', component: HomeComponent, children: [
    ...freelancerRoutes
  ]}
];

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    FreelancerModule,
    MatDialogModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    RouterModule.forRoot(routes)
  ],
  exports: [HomeComponent]
})
export class LayoutModule { }
