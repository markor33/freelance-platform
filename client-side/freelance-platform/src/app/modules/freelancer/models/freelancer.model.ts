import { Contact } from "../../shared/models/contact.model";
import { DateRange } from "../../shared/models/date-range.model";
import { ExperienceLevel } from "../../shared/models/experience-level.model";
import { LanguageKnowledge } from "../../shared/models/language.model";
import { Profession, Skill } from "../../shared/models/profession.mode";

export class Freelancer {
    id: string = '';
    firstName: string = '';
    lastName: string = '';
    contact: Contact = new Contact();
    profileSummary: ProfileSummary = new ProfileSummary();
    hourlyRate: HourlyRate = new HourlyRate();
    isPublic: boolean = false;
    joined: Date = new Date();
    availability: Availability = Availability.FULL_TIME;
    languageKnowledges: LanguageKnowledge[] = new Array();
    profession: Profession = new Profession();
    skills: Skill[] = new Array();
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
