using Microsoft.AspNetCore.SignalR;
using usecase_03_design_mini_twitter.DTOs;
using usecase_03_design_mini_twitter.Interfaces;
using usecase_03_design_mini_twitter.Models;
using usecase_03_design_mini_twitter.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITwitterService, TwitterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/tweet", (CreateTweetDto tweetDto, ITwitterService twitterService) =>
{
    if (tweetDto == null || tweetDto.UserId <= 0 || tweetDto.TweetId <= 0)
        return Results.BadRequest("problem creating tweet.");
    twitterService.PostTweet(tweetDto.UserId, tweetDto.TweetId);
    return Results.Created($"/api/tweet/{tweetDto.TweetId}", new { success = true });
}).WithName("PostTweet").Produces(201).Produces(400);

app.MapGet($"/api/feed/{{userId:int}}", (int userId, ITwitterService twitterService) =>
{
    var feed = twitterService.GetNewsFeed(userId);
    return Results.Ok(feed);
}).WithName("GetFeed").Produces<List<int>>(200);

app.MapPost("/api/follow", (FollowRequestDto followReqDto, ITwitterService twitterService) =>
{
    if(followReqDto == null || followReqDto.FollowerId  <= 0 || followReqDto.FolloweeId <= 0) 
        return Results.BadRequest("problem creating follow request.");
    
    twitterService.Follow(followReqDto.FollowerId, followReqDto.FolloweeId);
    return Results.Ok(new {success = true});
}).WithName("FollowUser").Produces(200).Produces(400);

app.MapPost("/api/unfollow", (FollowRequestDto followReqDto, ITwitterService twitterService) =>
{
    if(followReqDto == null || followReqDto.FollowerId  <= 0 || followReqDto.FolloweeId <= 0) 
        return Results.BadRequest("problem creating follow request.");
    
    twitterService.Unfollow(followReqDto.FollowerId, followReqDto.FolloweeId);
    return Results.Ok(new {success = true});
}).WithName("UnfollowUser").Produces(200).Produces(400);


app.Run();