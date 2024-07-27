import { Sentiment } from "../enums/sentiment";
import { UserModel } from "./user.model";

export class MessageModel{
    text: string = "";
    time: string = "";
    sentiment: Sentiment = Sentiment.Mixed;
    userId: string = "";
    user: UserModel = new UserModel();
}