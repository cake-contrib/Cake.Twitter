using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Twitter
{
    /// <summary>
    /// Contains aliases related to Twitter
    /// </summary>
    [CakeAliasCategory("Twitter")]
    public static class TwitterAliases
    {
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