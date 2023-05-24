export class User {
    systemId: string = '';
    domainId: string = '';
    firstName: string = '';
    lastName: string = '';
    professionId: string = '';
    role: string = '';

    constructor(systemId: string, domainId: string, firstName: string, lastName: string, role: string, professionId: string) {
        this.systemId = systemId;
        this.domainId = domainId;
        this.firstName = firstName;
        this.lastName = lastName;
        this.role = role;
        this.professionId = professionId;
    }

    addDomainData(domainId: string, firstName: string, lastName: string) {
        this.domainId = domainId;
        this.firstName = firstName;
        this.lastName = lastName;
    }
    
}