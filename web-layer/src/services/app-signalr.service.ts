import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import * as signalR from '@microsoft/signalr';
import { MessageModel } from '../models/message.model';
import { Sentiment } from '../enums/sentiment';
import { environment } from '../environments/environment.prod';
import { UserModel } from '../models/user.model';


@Injectable({
  providedIn: 'root'
})
export class AppSignalrService {
  public hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${environment.url}/chat`)
    .configureLogging(signalR.LogLevel.Information)
    .build();

  public messages$ = new BehaviorSubject<any>([]);
  public connectedUsers$ = new BehaviorSubject<UserModel[]>([]);
  public messages: any[] = [];
  public users: UserModel[] = [];

  constructor() {

    this.onMessageReceive();

    this.onReceiveConnectedUsers();

    this.onLoadMessages();

    this.onUserJoined();
  }

  onUserJoined() {
    this.hubConnection.on("UserJoined", (message: MessageModel) => {
      this.messages = [...this.messages, message];
      this.messages$.next(this.messages);
    })
  }

  onLoadMessages(): void {
    this.hubConnection.on("LoadMessages", (loadedMessages: MessageModel[]) => {
      const newMessages = loadedMessages.map(m => ({
        text: m.text,
        time: m.time,
        sentiment: m.sentiment,
        userId: m.userId,
        user: m.user
      }));
      this.messages = [...this.messages, ...newMessages];
      this.messages$.next(this.messages);
    });
  }

  onReceiveConnectedUsers() {
    this.hubConnection.on("ReceiveConnectedUsers", (users: UserModel[]) => {
      console.log(users);
      this.connectedUsers$.next(users);
    });
  }

  onMessageReceive() {
    this.hubConnection.on("ReceiveMessage", (message: MessageModel) => {
      const newMessage = new MessageModel();
      newMessage.text = message.text,
        newMessage.sentiment = message.sentiment,
        newMessage.time = message.time,
        newMessage.userId = message.userId
      newMessage.user = message.user

      this.messages = [...this.messages, newMessage];
      this.messages$.next(this.messages);
    });
  };


  public async startConnection() {
    try {
      await this.hubConnection.start()
    } catch (error) {
      console.log(error);
    }
  }

  public async joinRoom(user: string) {
    await this.hubConnection.start()
    return this.hubConnection.invoke("JoinChat", user);
  }

  public async sendMessage(message: string) {
    this.hubConnection.invoke('SendMessage', message);
  }

  public async leaveChat() {
    return this.hubConnection.stop();
  }
}
