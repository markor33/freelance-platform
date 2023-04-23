import { Contact } from "../../shared/models/contact.model";
import { ExperienceLevel } from "../../shared/models/experience-level.model";
import { LanguageProficiencyLevel } from "../../shared/models/language.model";
import { ProfileSummary, HourlyRate, Availability } from "./freelancer.model";

export class CreateFreelancerCommand {
    firstName: string = '';
    lastName: string = '';
    contact: Contact = new Contact();
    profileSummary: ProfileSummary = new ProfileSummary();
    hourlyRate: HourlyRate = new HourlyRate();
    isProfilePublic: boolean = false;
    availability: Availability = Availability.FULL_TIME;
    professionId: string = '';
    experienceLevel: ExperienceLevel = ExperienceLevel.JUNIOR;
    languageId : number = 0;
    languageProficiencyLevel: LanguageProficiencyLevel = LanguageProficiencyLevel.CONVERSATIONAL;
}