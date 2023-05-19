import { Injectable } from "@angular/core";
import { PaymentType } from "../../job/models/payment.model";
import { ExperienceLevel } from "../models/experience-level.model";
import { JobStatus } from "../../job/models/job.model";
import { ProposalStatus } from "../../job/models/proposal.model";

@Injectable({
    providedIn: 'root'
})
export class EnumConverter {

    paymentTypeToString(paymentType: PaymentType): string {
        switch(paymentType) {
            case PaymentType.FIXED_RATE:
                return 'Fixed';
            case PaymentType.HOURLY_RATE:
                return 'Hourly';
        }
    }

    experienceLevelToString(experienceLevel: ExperienceLevel): string {
        switch(experienceLevel) {
            case ExperienceLevel.JUNIOR:
                return 'Junior';
            case ExperienceLevel.MEDIOR:
                return 'Medior';
            case ExperienceLevel.SENIOR:
                return 'Senior';
        }
    }

    jobStatusToString(jobStatus: JobStatus): string {
        switch(jobStatus) {
            case JobStatus.LISTED:
                return 'Listed';
            case JobStatus.IN_PROGRESS:
                return 'In progress';
            case JobStatus.DONE:
                return 'Done';
            case JobStatus.REMOVED:
                return 'Removed';
        }
    }

    proposalStatusToString(proposalStatus: ProposalStatus): string {
        switch(proposalStatus) {
            case ProposalStatus.SENT:
                return 'Sent';
            case ProposalStatus.INTERVIEW:
                return 'Interview';
            case ProposalStatus.CLIENT_APPROVED:
                return 'Approved';
            case ProposalStatus.FREELANCER_APPROVED:
                return 'Contract';
        }
    }
}