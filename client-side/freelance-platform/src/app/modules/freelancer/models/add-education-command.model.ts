import { DateRange } from "../../shared/models/date-range.model";

export class AddEducationCommand {
    schoolName: string = '';
    degree: string = '';
    start : Date = new Date();
    end: Date = new Date();
}