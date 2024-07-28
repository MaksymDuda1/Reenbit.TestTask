import { AfterViewChecked, Component, ElementRef, input, OnInit, ViewChild } from '@angular/core';
import { AppSignalrService } from '../../services/app-signalr.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LocalService } from '../../services/local.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { MessageModel } from '../../models/message.model';
import { Sentiment, sentimentToString } from '../../enums/sentiment';
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
  messages: MessageModel[] = [];
  loggedInUserName = "";
  isLoading = true;

  @ViewChild('scroll') private scrollContainer!: ElementRef;

  getSentiment(sentiment: Sentiment){
    return sentimentToString(sentiment);
  }

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
      });
  }

  private subscribeToSignalR(): void {
    this.signalRService.messages$.subscribe(data => this.messages = data);
    this.signalRService.connectedUsers$.subscribe(data => this.users = data);
  }

  ngAfterViewChecked(): void {
    this.scrollContainer.nativeElement.scrollTop = this.scrollContainer.nativeElement.scrollHeight;
  }

  ngOnInit(): void {
    const token = this.localService.get(LocalService.AuthTokenName);
    if (token) {
      const decodedData = this.jwtHelperService.decodeToken(token);
      const user = decodedData.nameid;
      this.signalRService.joinRoom(user)
        .then(() => {
          this.subscribeToSignalR();
          this.isLoading = false;
        })
    } else {
      this.errorMessage = "No valid token found. Please log in.";
      this.isLoading = false;
    }
  }
}
