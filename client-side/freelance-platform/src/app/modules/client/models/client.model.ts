import { Contact } from "../../shared/models/contact.model";

export class Client {
    id : string = '';
    firstName: string = '';
    lastName: string = '';
    contact: Contact = new Contact();
}