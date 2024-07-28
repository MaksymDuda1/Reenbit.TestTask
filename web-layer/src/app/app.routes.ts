import { Routes } from '@angular/router';
import { ChatComponent } from './chat/chat.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { authGuard } from '../guards/auth.guard';

export const routes: Routes = [
    {path: "login", component: LoginComponent},
    {path: "registration", component: RegistrationComponent},
    {path: "chat", component: ChatComponent, canActivate: [authGuard]},
    {path: "home", component: HomeComponent, canActivate: [authGuard]},
    {path: "**", redirectTo: "/login"}
];
