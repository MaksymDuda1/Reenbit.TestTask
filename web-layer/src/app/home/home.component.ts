import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { LocalService } from '../../services/local.service';
import { AppSignalrService } from '../../services/app-signalr.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  constructor() { }
  router = inject(Router);
  localService = inject(LocalService);
  signalRService = inject(AppSignalrService);
  jwtHelperService = inject(JwtHelperService);

  errorMessage = "";

  onJoinRoom() {
    this.router.navigate(['chat']);
  }
}
