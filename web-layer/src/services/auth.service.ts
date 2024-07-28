import { Injectable } from "@angular/core";

import { LoginModel } from "../models/login.model";
import { RegistrationModel } from "../models/registration.model";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { environment } from "../environments/environment";

@Injectable({providedIn: "root"})
export class AuthService{
    constructor(private client: HttpClient){

    }

    private path: string = `${environment.url}/api/auth`;

    login(loginModel: LoginModel): Observable<any> {
        return this.client.post<any>(`${this.path}/login`, loginModel);
    }

    registration(registrationModel: RegistrationModel) : Observable<any>{
        return this.client.post<any>(`${this.path}/registration`, registrationModel);    
    }
}