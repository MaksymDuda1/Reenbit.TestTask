import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import * as signalR from '@microsoft/signalr';


@Injectable({
  providedIn: 'root'
})
export class AppSignalrService {
  public hubConnection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:8000/chat')
    .configureLogging(signalR.LogLevel.Information)
    .build();

  public messages$ = new BehaviorSubject<any>([]);
  public connectedUsers$ = new BehaviorSubject<string[]>([]);
  public messages: any[] = [];
  public users: string[] = [];

  constructor() {
  
    this.hubConnection.on("ReceiveMessage", (user: string, message: string, messageTime: string) => {
      this.messages = [...this.messages, { user, message, messageTime }];
      this.messages$.next(this.messages);
    });

    this.hubConnection.on("ReceiveConnectedUsers", (users: any) => {
      this.connectedUsers$.next(users);
    });

    this.hubConnection.on("LoadMessages", (loadedMessages: any[]) => {
      const newMessages = loadedMessages.map(msg => ({
        user: msg.userId,
        message: msg.text,
        messageTime: msg.time
      }))
      this.messages = [...this.messages, ...newMessages];
      this.messages$.next(this.messages);
    })


    this.hubConnection.on("UserJoined", (user: string, message: string) => {
      this.messages = [...this.messages, { user, message }];
      this.messages$.next(this.messages);
    })
  }
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
  getConnectedUsers() {
    this.hubConnection.invoke("GetConnectedUsers").catch(err => console.error(err));
  }
}
