using usecase_03_design_mini_twitter.Interfaces;
using usecase_03_design_mini_twitter.Services;
using System.Linq;

namespace mini_twitter.Tests;

public class TwitterServiceTest
{
    private readonly ITwitterService _twitterService;

    public TwitterServiceTest()
    {
        _twitterService = new TwitterService();
    }

    [Fact]
    public void PostTweet_shouldAddTweet()
    {
        _twitterService.PostTweet(1, 101);
        var feed = _twitterService.GetNewsFeed(1);
        Assert.NotNull(feed);
        Assert.Single(feed);
        Assert.Equal(101, feed[0]);
    }

    [Fact]
    public void GetNewsFeed_ShouldReturnLatestTweets()
    {
        _twitterService.PostTweet(1, 101);
        _twitterService.PostTweet(1, 102);
        _twitterService.PostTweet(1, 103);

        var feed = _twitterService.GetNewsFeed(1);

        Assert.NotNull(feed);
        Assert.Equal(3, feed.Count);
        Assert.Equal(new[] { 103, 102, 101 }, feed);
    }
    
    [Fact]
    public void Follow_ShouldIncludeFolloweeTweets()
    {
        _twitterService.PostTweet(1, 101);
        _twitterService.PostTweet(2, 201);

        _twitterService.Follow(1, 2);
        var feed = _twitterService.GetNewsFeed(1);

        Assert.Equal(2, feed.Count);
        Assert.Contains(101, feed);
        Assert.Contains(201, feed);
    }
    
    [Fact]
    public void Unfollow_ShouldRemoveFolloweeTweets()
    {
        _twitterService.PostTweet(1, 101);
        _twitterService.PostTweet(2, 201);

        _twitterService.Follow(1, 2);
        _twitterService.Unfollow(1, 2);

        var feed = _twitterService.GetNewsFeed(1);
        Assert.Single(feed);
        Assert.Equal(101, feed[0]);
    }

    [Fact]
    public void GetNewsFeed_ShouldReturnTop10TweetsOnly()
    {
        for (int i = 1; i <= 15; i++)
        {
            _twitterService.PostTweet(1, i + 100);
        }
        var feed = _twitterService.GetNewsFeed(1);
        Assert.NotNull(feed);
        Assert.Equal(10, feed.Count);
        Assert.Equal(Enumerable.Range(106, 10).Reverse().ToList(), feed);
    }
}