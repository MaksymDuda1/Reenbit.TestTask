import { AfterViewChecked, Component, ElementRef, input, OnInit, ViewChild } from '@angular/core';
import { AppSignalrService } from '../../services/app-signalr.service';
import { subscribe } from 'node:diagnostics_channel';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LocalService } from '../../services/local.service';
import { JwtHelperService } from '@auth0/angular-jwt';
@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss'
})
export class ChatComponent implements OnInit, AfterViewChecked {
  constructor(
    private signalRService: AppSignalrService,
    private router: Router,
    private localService: LocalService,
    private jwtHelperService: JwtHelperService) {
    let token = this.localService.get(LocalService.AuthTokenName);
    if (token) {
      let decodedData = this.jwtHelperService.decodeToken(token);
      this.loggedInUserName = decodedData.nameid;
    }
  }
  errorMessage = "";
  inputedMessage = "";
  receivedMessage: string = '';
  users: any[] = [];
  messages: any[] = [];
  loggedInUserName = "";
  @ViewChild('scroll') private scrollContainer!: ElementRef;
  sendMessage() {
    if (this.inputedMessage != "") {
      this.signalRService.sendMessage(this.inputedMessage)
        .then(() => {
          this.inputedMessage = "";
        }).catch((err) => {
          console.log(err);
        });
    }
  }
  leaveChat() {
    this.signalRService.leaveChat()
      .then(() => {
        this.router.navigate(["home"])
      }).catch((err) => {
        console.log(err);
      })
  }
  ngAfterViewChecked(): void {
    this.scrollContainer.nativeElement.scrollTop = this.scrollContainer.nativeElement.scrollHeight;
  }
  ngOnInit(): void {
    let token = this.localService.get(LocalService.AuthTokenName);
    if (token) {
      var decodedData = this.jwtHelperService.decodeToken(token);
      let user = decodedData.nameid;
      this.signalRService.joinRoom(user)
        .then(() => {
          this.signalRService.messages$.subscribe(data => {
            this.messages = data;
          });
          this.signalRService.connectedUsers$.subscribe(data => {
            this.users = data;
          })
        }).catch((error: any) => {
          this.errorMessage = error;
        });
    }
  }
}