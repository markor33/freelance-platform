import { Injectable } from "@angular/core";
import { NotificationContent } from "../../models/notification-content.model";
import { NotificationHandler } from "../notification-handler";
import { FreelancerAcceptedProposalNotification } from "./freelancer-accepted-proposal-notification.model";
import { MatDialog } from "@angular/material/dialog";
import { ProposalInfoDialogComponent } from "src/app/modules/job/proposals-management/dialogs/proposal-info-dialog/proposal-info-dialog.component";

@Injectable({
    providedIn: 'root'
})
export class FreelancerAcceptedProposalNotificationHandler implements NotificationHandler {

    constructor(private dialog: MatDialog) { }

    getType(): string {
        return FreelancerAcceptedProposalNotification.name;
    }

    getContent(data: FreelancerAcceptedProposalNotification): NotificationContent {
        return {
            title: 'Freelancer accepted proposal',
            description: `Freelancer has accpeted proposal for '${data.JobTitle}' job`
        }
    }

    handle(data: FreelancerAcceptedProposalNotification): void {
        this.dialog.open(ProposalInfoDialogComponent, {
            width: '50%',
            height: '80%',
            data: { jobId: data.JobId, proposalId: data.ProposalId }
        });
    }

}