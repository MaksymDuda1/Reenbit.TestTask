import { Routes } from '@angular/router';
import { ChatComponent } from './chat/chat.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';

export const routes: Routes = [
    {path: "", redirectTo:"/login", pathMatch: "full"},
    {path: "login", component: LoginComponent},
    {path: "registration", component: RegistrationComponent},
    {path: "chat", component: ChatComponent},
    {path: "home", component: HomeComponent}
];
