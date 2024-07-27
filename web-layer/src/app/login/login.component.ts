import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { LoginModel } from '../../models/login.model';
import { LocalService } from '../../services/local.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})

export class LoginComponent {
  constructor(private authService: AuthService,
    private localService: LocalService,
    private jwtHelperService: JwtHelperService
  ) { }

  loginModel = new LoginModel();
  errorMessage = "";

  onLogin(){
    this.authService.login(this.loginModel).subscribe((data : any) => {
      this.localService.put(LocalService.AuthTokenName, data.accessToken);
        let decodedData = this.jwtHelperService.decodeToken(data.accessToken);

        if(decodedData.role == "Admin")
          window.location.href = '/admin';
        else
          window.location.href = '/home';
    },
    (errorResponse: any) => {
      this.errorMessage = errorResponse;
      })
  }
}