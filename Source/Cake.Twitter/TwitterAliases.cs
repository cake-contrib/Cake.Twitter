using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Twitter
{
    /// <summary>
    /// <para>Contains functionality related to <see href="https://dev.twitter.com/rest/public">Twitter REST API</see>.</para>
    /// <para>
    /// In order to use the commands for this addin, include the following in your build.cake file to download and
    /// reference from NuGet.org:
    /// <code>
    /// #addin Cake.Twitter
    /// </code>
    /// </para>
    /// </summary>
    [CakeAliasCategory("Twitter")]
    public static class TwitterAliases
    {
        /// <summary>
        /// Send a Tweet using the Twitter REST API.
        /// </summary>
        /// <param name="context">The Cake Context</param>
        /// <param name="oAuthConsumerKey">The Twitter Consumer Key</param>
        /// <param name="oAuthConsumerSecret">The Twitter Consumer Secret</param>
        /// <param name="accessToken">The Twitter Application Access Token</param>
        /// <param name="accessTokenSecret">The Twitter Application Token Secret</param>
        /// <param name="message">The message to tweet.</param>
        /// <example>
        /// <code>
        ///     var oAuthConsumerKey        = EnvironmentVariable("TWITTER_CONSUMER_KEY");
        ///     var oAuthConsumerSecret = EnvironmentVariable("TWITTER_CONSUMER_SECRET");
        ///     var accessToken = EnvironmentVariable("TWITTER_ACCESS_TOKEN");
        ///     var accessTokenSecret = EnvironmentVariable("TWITTER_ACCESS_TOKEN_SECRET");
        ///
        ///     TwitterSendTweet(oAuthConsumerKey, oAuthConsumerSecret, accessToken, accessTokenSecret, "Testing, 1, 2, 3");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeNamespaceImportAttribute("Cake.Twitter")]
        public static void TwitterSendTweet(this ICakeContext context, string oAuthConsumerKey, string oAuthConsumerSecret, string accessToken, string accessTokenSecret, string message)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var twitterProvider = new TwitterProvider(context, oAuthConsumerKey, oAuthConsumerSecret, accessToken, accessTokenSecret);
            twitterProvider.SendTweet(message);
        }
    }
}