export class User {
    systemId: string = '';
    domainId: string = '';
    firstName: string = '';
    lastName: string = '';
    role: string = '';

    constructor(systemId: string, domainId: string, firstName: string, lastName: string, role: string) {
        this.systemId = systemId;
        this.domainId = domainId;
        this.firstName = firstName;
        this.lastName = lastName;
        this.role = role;
    }

    addDomainData(domainId: string, firstName: string, lastName: string) {
        this.domainId = domainId;
        this.firstName = firstName;
        this.lastName = lastName;
    }
    
}