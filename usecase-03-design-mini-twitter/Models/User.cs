namespace usecase_03_design_mini_twitter.Models;

public class User
{
    public int UserId { get; set; }
    public HashSet<int> Followees { get; set; }
    public LinkedList<Tweet> Tweets { get; set; }

    public User(int userId)
    {
        UserId = userId;
        Followees = new HashSet<int>();
        Tweets = new LinkedList<Tweet>();
    }
    public void AddTweet(Tweet tweet)
    {
        Tweets.AddFirst(tweet);
    }

    public void AddFollowee(int followeeId)
    {
        if (!Followees.Contains(followeeId))
        {
            Followees.Add(followeeId);
        }
    }

    public void RemoveFollowee(int followeeId)
    {
        if (Followees.Contains(followeeId))
        {
            Followees.Remove(followeeId);
        }
    }
}