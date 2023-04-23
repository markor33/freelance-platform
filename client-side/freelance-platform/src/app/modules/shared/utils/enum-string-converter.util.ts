import { PaymentType } from "../../job/models/payment.model";
import { ExperienceLevel } from "../models/experience-level.model";

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
}