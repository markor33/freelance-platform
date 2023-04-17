import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientCompleteRegistrationComponent } from './client-complete-registration/client-complete-registration.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { DialogModule } from '@angular/cdk/dialog';



@NgModule({
  declarations: [
    ClientCompleteRegistrationComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    MatButtonModule,
    DialogModule,
    MatInputModule
  ]
})
export class ClientModule { }
