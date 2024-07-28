export enum Sentiment{
    Positive,
    Neutral,
    Negative,
    Mixed
}

export function sentimentToString(sentiment: Sentiment){
    switch(sentiment){
        case Sentiment.Positive:
            return "Positive";
        case Sentiment.Neutral:
            return "Neutral";
        case Sentiment.Negative:
            return "Negative";
        case Sentiment.Mixed:
            return "Mixed";
        default:
            return "Mixed";
    }
}