import { MatDialog } from "@angular/material/dialog";
import { ProposalInfoDialogComponent } from "src/app/modules/job/proposals-management/dialogs/proposal-info-dialog/proposal-info-dialog.component";
import { NotificationContent } from "../../models/notification-content.model";
import { NotificationHandler } from "../notification-handler";
import { ClientAcceptedProposalNotification } from "./client-accepted-proposal-notification.model";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class ClientAcceptedProposalNotificationHandler implements NotificationHandler {

    constructor(private dialog: MatDialog) { }

    getType(): string {
        return ClientAcceptedProposalNotification.name;
    }

    getContent(data: ClientAcceptedProposalNotification): NotificationContent {
        return {
            title: 'Client accepted proposal',
            description: `Client has accpeted your proposal for '${data.JobTitle}' job`
        }
    }

    handle(data: ClientAcceptedProposalNotification): void {
        this.dialog.open(ProposalInfoDialogComponent, {
            width: '50%',
            height: '80%',
            data: { jobId: data.JobId, proposalId: data.ProposalId }
        });
    }

}