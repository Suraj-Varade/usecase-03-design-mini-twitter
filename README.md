# Mini Twitter (C# .NET Core)

This project is a **mini Twitter clone** implemented in C# using .NET 8.  
It demonstrates core social media features such as posting tweets, following/unfollowing users, and fetching user timelines.  
The implementation is based on a common **system design/LeetCode-style interview problem**.

## 🚀 Features
- Post a tweet
- Follow and unfollow users
- Retrieve recent tweets (timeline/news feed)
- In-memory data structures for simplicity
- Clean code following **SOLID principles** and **interfaces**

## 🛠️ Tech Stack
- **Language:** C#
- **Framework:** .NET 8 (Minimal API), XUnit (Test)
- **Architecture:** Interface-driven design, dependency injection, in-memory store  

## Project Structure
```text
usecase-03-design-mini-twitter/
│
├── mini-twitter.Tests/                # Test project
│   └── TwitterServiceTest.cs
│
└── usecase-03-design-mini-twitter/    # Main project
    ├── DTOs/                          # Data Transfer Objects
    │   ├── CreateTweetDto.cs
    │   └── FollowRequestDto.cs
    │
    ├── Interfaces/                    # Contracts / Abstractions
    │   └── ITwitterService.cs
    │
    ├── Models/                        # Domain Models
    │   ├── Tweet.cs
    │   └── User.cs
    │
    ├── Services/                      # Business Logic
    │   └── TwitterService.cs
    │
    ├── appsettings.json               # App configuration
    ├── appsettings.Development.json   # Dev-specific config
    ├── Program.cs                     # Entry point
    └── usecase-03-design-mini-twitter.http  # HTTP request file for testing
```

## ▶️ Getting Started
### 1. Clone the repo
```
git clone https://github.com/Suraj-Varade/usecase-03-design-mini-twitter.git

cd usecase-03-design-mini-twitter
```
### 2. Run the project
```
dotnet run
```
### 3. Test with HTTP client
You can use the included usecase-03-design-mini-twitter.http file (compatible with Rider/VS Code REST Client)
or tools like Postman / curl.

## ✅ Future Enhancements
- Persist data in a database (SQL/NoSQL)
- Add authentication/authorization
- Add pagination for timelines
- Dockerize the service for deployment
