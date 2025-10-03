using System.Collections.Concurrent;
using usecase_03_design_mini_twitter.Interfaces;
using usecase_03_design_mini_twitter.Models;

namespace usecase_03_design_mini_twitter.Services;

public class TwitterService : ITwitterService
{
    private long timeStamp = 0;
    private ConcurrentDictionary<int, User> userDict;

    public TwitterService()
    {
        userDict = new ConcurrentDictionary<int, User>();
    }

    private long NextTimeStamp()
    {
        return Interlocked.Increment(ref timeStamp);
    }

    public void PostTweet(int userId, int tweetId)
    {
        if (!userDict.ContainsKey(userId))
        {
            userDict.TryAdd(userId, new User(userId));
        }

        var user = userDict[userId];
        user.AddTweet(new Tweet(tweetId, NextTimeStamp()));
    }

    public List<int> GetNewsFeed(int userId)
    {
        var result = new List<int>(10);
        if (!userDict.TryGetValue(userId, out var user))
        {
            return result;
        }

        var pq = new PriorityQueue<LinkedListNode<Tweet>, long>();
        // self and followee tweets

        var snode = user.Tweets?.First;
        if (snode != null)
        {
            pq.Enqueue(snode, -snode.Value.TimeStamp);
        }

        var followees = user.Followees;
        foreach (var fid in followees)
        {
            if (userDict.TryGetValue(fid, out var fUser))
            {
                var node = fUser.Tweets.First;
                if (node != null)
                {
                    pq.Enqueue(node, -node.Value.TimeStamp);
                }
            }
        }

        while (result.Count < 10 && pq.Count > 0)
        {
            var node = pq.Dequeue();
            result.Add(node.Value.TweetId);
            if (node.Next != null)
            {
                pq.Enqueue(node.Next, -node.Next.Value.TimeStamp);
            }
        }

        return result;
    }

    public void Follow(int followerId, int followeeId)
    {
        if (!userDict.ContainsKey(followerId) || !userDict.ContainsKey(followeeId))
        {
            return;
        }

        var user = userDict[followerId];
        user.AddFollowee(followeeId);
    }

    public void Unfollow(int followerId, int followeeId)
    {
        if (!userDict.ContainsKey(followerId) || !userDict.ContainsKey(followeeId))
        {
            return;
        }

        var user = userDict[followerId];
        user.RemoveFollowee(followeeId);
    }
}