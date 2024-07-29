import { Injectable } from "@angular/core";

import { LoginModel } from "../models/login.model";
import { RegistrationModel } from "../models/registration.model";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { environment } from "../environments/environment.prod";

@Injectable({providedIn: "root"})
export class AuthService{
    constructor(private client: HttpClient){

    }

    private path: string = `${environment.url}/api/auth`;

    isValid(username: string, email: string, password: string): boolean {
        const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        return emailPattern.test(email) && password.length >= 8 && username.length > 0;
    }

    login(loginModel: LoginModel): Observable<any> {
        return this.client.post<any>(`${this.path}/login`, loginModel);
    }

    registration(registrationModel: RegistrationModel) : Observable<any>{
        return this.client.post<any>(`${this.path}/registration`, registrationModel);    
    }
}