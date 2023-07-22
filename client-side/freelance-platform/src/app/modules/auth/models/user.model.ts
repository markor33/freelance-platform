export class User {
    systemId: string = '';
    domainId: string = '';
    isProfileSetupCompleted : boolean = false;
    firstName: string = '';
    lastName: string = '';
    role: string = '';

    constructor(userClaims: any) {
        this.systemId = userClaims.sub;
        this.domainId = userClaims.domainUserId;
        this.firstName = userClaims.firstName;
        this.lastName = userClaims.lastName;
        this.role = userClaims.role;
    }
    
}