import { Contact } from "../../shared/models/contact.model";
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