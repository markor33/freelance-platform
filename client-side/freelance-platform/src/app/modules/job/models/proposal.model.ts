import { Answer } from "./answer.model";
import { Payment } from "./payment.model";

export class Proposal {
    id: string = '';
    text: string = '';
    payment: Payment = new Payment();
    status: ProposalStatus | null = null;
    created: Date = new Date();
    freelancerId: string = '';
    freelancer: any;
    answers: Answer[] = [];
}

export enum ProposalStatus {
    SENT,
    INTERVIEW,
    CLIENT_ACCEPTED,
    ACCEPTED
}
