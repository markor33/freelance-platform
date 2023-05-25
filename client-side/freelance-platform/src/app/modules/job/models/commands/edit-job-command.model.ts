import { ExperienceLevel } from "src/app/modules/shared/models/experience-level.model";
import { Payment } from "../payment.model";
import { Question } from "../question.model";

export class EditJobCommand {
    jobId: string = '';
    title: string = '';
    description: string = '';
    professionId: string = '';
    skills: string[] = [];
    experienceLevel: ExperienceLevel = ExperienceLevel.JUNIOR;
    payment: Payment = new Payment();
    questions: Question[] = new Array<Question>();
}