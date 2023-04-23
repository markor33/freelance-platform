import { Payment } from "./payment.model";

export class Proposal {
    id: string = '';
    text: string = '';
    payment: Payment = new Payment();
    status: ProposalStatus | null = null;
}

export enum ProposalStatus {
    SENT,
    INTERVIEW,
    CLIENT_ACCEPTED,
    ACCEPTED
}
