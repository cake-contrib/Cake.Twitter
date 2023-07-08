#r "bin\Debug\net6.0\Cake.Twitter.dll"

var oAuthConsumerKey        = EnvironmentVariable("TWITTER_CONSUMER_KEY");
var oAuthConsumerSecret     = EnvironmentVariable("TWITTER_CONSUMER_SECRET");
var accessToken             = EnvironmentVariable("TWITTER_ACCESS_TOKEN");
var accessTokenSecret       = EnvironmentVariable("TWITTER_ACCESS_TOKEN_SECRET");

try
{
    TwitterSendTweet(oAuthConsumerKey,
                    oAuthConsumerSecret,
                    accessToken,
                    accessTokenSecret,
                    "Testing, 1, 2, 3");
}
catch(Exception ex)
{
    Error("{0}", ex);
}