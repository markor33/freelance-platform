import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatComponent } from './chat/chat.component';
import { Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';

export const chatRoutes: Routes = [
  { path: 'chat', component: ChatComponent }  
];

@NgModule({
  declarations: [
    ChatComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    MatDividerModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatDialogModule,
    FormsModule
  ]
})
export class ChatModule { }
