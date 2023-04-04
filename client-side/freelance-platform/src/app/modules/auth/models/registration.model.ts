export class Registration {
    username: string = '';
    email: string = '';
    password: string = '';
    confirmPassword: string = '';
    role: Role = Role.FREELANCER;
}

export enum Role {
    FREELANCER = 0, 
    CLIENT = 1
}