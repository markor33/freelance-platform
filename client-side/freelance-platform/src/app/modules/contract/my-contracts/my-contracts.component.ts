import { Component, ViewChild } from '@angular/core';
import { ContractService } from '../services/contract.service';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from '../../auth/services/auth.service';
import { EnumConverter } from '../../shared/utils/enum-string-converter.util';
import { JobInfoDialogComponent } from '../../job/jobs-management/dialogs/job-info-dialog/job-info-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-my-contracts',
  templateUrl: './my-contracts.component.html',
  styleUrls: ['./my-contracts.component.scss']
})
export class MyContractsComponent {

  role: string = '';
  userId: string = '';

  contracts: MatTableDataSource<any> = new MatTableDataSource();
  
  @ViewChild(MatSort) sort!: MatSort;

  public displayedColumns: string[] = ['job', 'freelancer', 'status', 'started', 'finished', 'payment'];

  constructor(
    private contractService: ContractService,
    private authService: AuthService,
    private dialog: MatDialog,
    public enumConverter: EnumConverter
  ) {
    this.authService.userObserver.subscribe((user) => {
      this.userId = user?.domainId as string;
      this.role = user?.role as string;
    });
  }

  ngOnInit() {
    if (this.role === 'CLIENT')
      this.contractService.getByClient(this.userId).subscribe((contracts) => this.contracts.data = contracts);
    else if (this.role === 'FREELANCER') {
      this.displayedColumns.splice(1, 1);
      this.contractService.getByFreelancer().subscribe((contracts) => this.contracts.data = contracts);
    }
  }

  openJobInfoDialog(jobId: string) {
    JobInfoDialogComponent.open(this.dialog, jobId);
  }

}
