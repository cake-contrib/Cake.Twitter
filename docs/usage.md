# Build Script

Within your build script, you will need to add the following (normally near the top of the file):

```csharp
#addin Cake.Twitter
```

And then you will be able to utilise the TwitterTweet Method Alias.  An example of how this can be done is shown below:

```csharp
Task("Tweet")
    .Does(() =>
{
    var oAuthConsumerKey        = EnvironmentVariable("TWITTER_CONSUMER_KEY");
    var oAuthConsumerSecret     = EnvironmentVariable("TWITTER_CONSUMER_SECRET");
    var accessToken             = EnvironmentVariable("TWITTER_ACCESS_TOKEN");
    var accessTokenSecret       = EnvironmentVariable("TWITTER_ACCESS_TOKEN_SECRET");

    TwitterSendTweet(oAuthConsumerKey, oAuthConsumerSecret, accessToken, accessTokenSecret, "Testing, 1, 2, 3");
})
```

Here, all the necessary tokens are retrieved from Environment Variables on the system where the build is executing.