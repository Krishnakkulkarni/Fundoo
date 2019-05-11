export class User {
    UserName: string;
    Password: string;
    Email: string;
    FirstName: string;
    LastName: string;
}
export class UserLogin {
    UserName: string;
    Password: string;
}
export class Userresettpassword {
    Email: string;
    Password: string;
    ConfirmPassword: string;
}
export class Userforgotpassword {
    Email: string;
}
