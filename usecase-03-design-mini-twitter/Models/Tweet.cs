namespace usecase_03_design_mini_twitter.Models;

public class Tweet
{
    public int TweetId { get; }
    public long TimeStamp { get; }

    public Tweet(int tweetId, long timeStamp) 
    {
        this.TweetId = tweetId;
        this.TimeStamp = timeStamp;
    }
}