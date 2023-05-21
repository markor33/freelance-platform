import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ContractManagementComponent } from './contract-management/contract-management.component';
import { RouterModule, Routes } from '@angular/router';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MyContractsComponent } from './my-contracts/my-contracts.component';

export const contractRoutes: Routes = [
  { path: 'job/:id/contract-management', component: ContractManagementComponent },
  { path: 'my-contracts', component: MyContractsComponent }
];

@NgModule({
  declarations: [
    ContractManagementComponent,
    MyContractsComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    MatTableModule,
    RouterModule,
    MatIconModule,
    MatButtonModule
  ]
})
export class ContractModule { }
