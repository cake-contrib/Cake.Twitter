//------------------------------------------------------------------------------
// twitter.cake — exercise script for Cake.Twitter.
//
// HEADS UP: this exercise will likely NOT successfully post a tweet.
//
//   1. Twitter retired the Free API tier. Calling the v2 /2/tweets endpoint
//      now requires Pay-Per-Use or Basic+ ($100+/month), so consumers using
//      this addin without a paid plan will get HTTP 403 "client-not-enrolled"
//      back from Twitter (regardless of whether the OAuth signature is
//      otherwise valid).
//   2. Even on a paying account, the Twitter app must be attached to a
//      Project in the Developer Portal before /2/tweets will accept posts.
//   3. The addin currently has a SILENT-FAILURE bug: TwitterProvider.SendRequest
//      does not call EnsureSuccessStatusCode, and TwitterAliases.TwitterSendTweet
//      discards the response body. So the call below can return without an
//      exception even when Twitter has rejected the post entirely. Don't
//      treat absence of an exception here as proof that a tweet was sent —
//      check the actual Twitter account.
//
// The fix for (3) is queued separately in the cake-addins-update workspace
// (separate small PR, targeting Cake.Twitter's current Cake major). Once
// that lands, this script will surface real errors instead of swallowing
// them.
//
// SCOPE OF EXERCISE: there is only one public alias in this addin —
// TwitterSendTweet. Nothing else to call. The single-tweet shape below
// covers it.
//------------------------------------------------------------------------------

#r "bin\Debug\net10.0\Cake.Twitter.dll"

var oAuthConsumerKey        = EnvironmentVariable("TWITTER_CONSUMER_KEY");
var oAuthConsumerSecret     = EnvironmentVariable("TWITTER_CONSUMER_SECRET");
var accessToken             = EnvironmentVariable("TWITTER_ACCESS_TOKEN");
var accessTokenSecret       = EnvironmentVariable("TWITTER_ACCESS_TOKEN_SECRET");

if (string.IsNullOrEmpty(oAuthConsumerKey)
    || string.IsNullOrEmpty(oAuthConsumerSecret)
    || string.IsNullOrEmpty(accessToken)
    || string.IsNullOrEmpty(accessTokenSecret))
{
    Warning("One or more TWITTER_* environment variables are unset. The call below will be sent with empty credentials and will fail at the API layer.");
}

try
{
    TwitterSendTweet(oAuthConsumerKey,
                    oAuthConsumerSecret,
                    accessToken,
                    accessTokenSecret,
                    "Testing, 1, 2, 3");
    Information("TwitterSendTweet returned without an exception.");
    Information("NOTE: this does NOT prove the tweet was sent — see the silent-failure caveat at the top of this file.");
}
catch (Exception ex)
{
    Error("TwitterSendTweet threw: {0}", ex);
}
