import { Component, Inject } from '@angular/core';
import { Proposal } from '../../../models/proposal.model';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EnumConverter } from 'src/app/modules/shared/utils/enum-string-converter.util';
import { ProposalService } from '../../../services/proposal.service';

@Component({
  selector: 'app-proposal-info-dialog',
  templateUrl: './proposal-info-dialog.component.html',
  styleUrls: ['./proposal-info-dialog.component.scss']
})
export class ProposalInfoDialogComponent {

  proposal: Proposal = new Proposal();

  constructor(
    public enumConverter: EnumConverter,
    @Inject(MAT_DIALOG_DATA) public data: {proposal: Proposal},
    private proposalService: ProposalService) {
      this.proposal = data.proposal;
  }

  ngOnInit() {
    this.proposalService.getAnswers(this.proposal.id).subscribe(answers => this.proposal.answers = answers);
  }


}
