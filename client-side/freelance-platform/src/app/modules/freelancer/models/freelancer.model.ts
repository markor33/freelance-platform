import { Contact } from "../../shared/models/contact.model";
import { DateRange } from "../../shared/models/date-range.model";
import { LanguageKnowledge } from "../../shared/models/language.model";
import { Profession } from "../../shared/models/profession.mode";

export class Freelancer {
    firstName: string = '';
    lastName: string = '';
    contact: Contact = new Contact();
    profileSummary: ProfileSummary = new ProfileSummary();
    hourlyRate: HourlyRate = new HourlyRate();
    isPublic: boolean = false;
    availability: Availability = Availability.FULL_TIME;
    languageKnowledges: LanguageKnowledge[] = new Array();
    profession: Profession = new Profession();
    experienceLevel: ExperienceLevel = ExperienceLevel.JUNIOR;
    educations: Education[] = new Array();
    certifications: Certification[] = new Array();
    employments: Employment[] = new Array();
}

export class ProfileSummary {
    title: string = '';
    description: string = '';
}

export class HourlyRate {
    amount: number = 0.0;
    currency: string = 'EUR';
}

export enum Availability {
    FULL_TIME,
    PART_TIME
}

export enum ExperienceLevel {
    JUNIOR,
    MEDIOR,
    SENIOR
}

export class Education {
    id: string = '';
    schoolName: string = '';
    degree: string = '';
    attended: DateRange = new DateRange();
}

export class Certification {
    id: string = '';
    name: string = '';
    provider: string = '';
    attended: DateRange = new DateRange();
    description: string = '';
}

export class Employment {
    id: string = '';
    title: string = '';
    company: string = '';
    period: DateRange = new DateRange();
    description: string = '';
}
