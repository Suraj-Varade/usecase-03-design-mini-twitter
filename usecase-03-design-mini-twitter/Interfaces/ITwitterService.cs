namespace usecase_03_design_mini_twitter.Interfaces;

public interface ITwitterService
{
    void PostTweet(int userId, int tweetId);
    List<int> GetNewsFeed(int userId); //get 10 most recent tweet ids.
    void Follow(int followerId, int followeeId);
    void Unfollow(int followerId, int followeeId);
}