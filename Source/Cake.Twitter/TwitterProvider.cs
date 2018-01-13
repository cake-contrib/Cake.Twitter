﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Cake.Core;

namespace Cake.Twitter
{
    // The code within this TwitterProvider has been based almost exclusively on the work that was done by Alex (That Software Dude)
    // based on this blog post:
    // http://www.thatsoftwaredude.com/content/6289/how-to-post-a-tweet-using-c-for-single-user

    /// <summary>
    /// Contains functionality related to Twitter API
    /// </summary>
    public sealed class TwitterProvider
    {
        private string _oAuthConsumerKey = string.Empty;
        private string _oAuthConsumerSecret = string.Empty;
        private string _accessToken = string.Empty;
        private string _accessTokenSecret = string.Empty;
        private string oAuthUrl = "https://api.twitter.com/1.1/statuses/update.json";

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterProvider"/> class.
        /// </summary>
        /// <param name="context">The Cake Context</param>
        /// <param name="oAuthConsumerKey">The Twitter Consumer Key</param>
        /// <param name="oAuthConsumerSecret">The Twitter Consumer Secret</param>
        /// <param name="accessToken">The Twitter Application Access Token</param>
        /// <param name="accessTokenSecret">The Twitter Application Token Secret</param>
        public TwitterProvider(ICakeContext context, string oAuthConsumerKey, string oAuthConsumerSecret, string accessToken, string accessTokenSecret)
        {
            _oAuthConsumerKey = oAuthConsumerKey;
            _oAuthConsumerSecret = oAuthConsumerSecret;
            _accessToken = accessToken;
            _accessTokenSecret = accessTokenSecret;
        }

        /// <summary>
        /// Send a tweet with the specified message.
        /// </summary>
        /// <param name="message">The message to tweet.</param>
        public void SendTweet(string message)
        {
            string authHeader = GenerateAuthorizationHeader(message);
            string postBody = "status=" + Uri.EscapeDataString(message);
            byte[] content = Encoding.UTF8.GetBytes(postBody);

            var request = new ByteArrayContent(content);

            request.Headers.Add("Authorization", authHeader);
            request.Headers.Add("UserAgent", "OAuth gem v0.4.4");
            request.Headers.Add("Host", "api.twitter.com");
            request.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded;charset=UTF-8");
            request.Headers.ContentLength = content.Length;

            var messageHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            using (var client = new HttpClient(messageHandler))
            {
                client.PostAsync(oAuthUrl, request);
            }
        }

        private static double ConvertToUnixTimestamp(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        private string GenerateAuthorizationHeader(string status)
        {
            string signatureMethod = "HMAC-SHA1";
            string version = "1.0";
            string nonce = GenerateNonce();
            double timestamp = ConvertToUnixTimestamp(DateTime.Now);
            string dst = string.Empty;

            dst = string.Empty;
            dst += "OAuth ";
            dst += string.Format("oauth_consumer_key=\"{0}\", ", Uri.EscapeDataString(_oAuthConsumerKey));
            dst += string.Format("oauth_nonce=\"{0}\", ", Uri.EscapeDataString(nonce));
            dst += string.Format("oauth_signature=\"{0}\", ", Uri.EscapeDataString(GenerateOauthSignature(status, nonce, timestamp.ToString())));
            dst += string.Format("oauth_signature_method=\"{0}\", ", Uri.EscapeDataString(signatureMethod));
            dst += string.Format("oauth_timestamp=\"{0}\", ", timestamp);
            dst += string.Format("oauth_token=\"{0}\", ", Uri.EscapeDataString(_accessToken));
            dst += string.Format("oauth_version=\"{0}\"", Uri.EscapeDataString(version));
            return dst;
        }

        private string GenerateOauthSignature(string status, string nonce, string timestamp)
        {
            string signatureMethod = "HMAC-SHA1";
            string version = "1.0";
            string result = string.Empty;
            string dst = string.Empty;

            dst += string.Format("oauth_consumer_key={0}&", Uri.EscapeDataString(_oAuthConsumerKey));
            dst += string.Format("oauth_nonce={0}&", Uri.EscapeDataString(nonce));
            dst += string.Format("oauth_signature_method={0}&", Uri.EscapeDataString(signatureMethod));
            dst += string.Format("oauth_timestamp={0}&", timestamp);
            dst += string.Format("oauth_token={0}&", Uri.EscapeDataString(_accessToken));
            dst += string.Format("oauth_version={0}&", Uri.EscapeDataString(version));
            dst += string.Format("status={0}", Uri.EscapeDataString(status));

            string signingKey = string.Empty;
            signingKey = string.Format("{0}&{1}", Uri.EscapeDataString(_oAuthConsumerSecret), Uri.EscapeDataString(_accessTokenSecret));

            result += "POST&";
            result += Uri.EscapeDataString(oAuthUrl);
            result += "&";
            result += Uri.EscapeDataString(dst);

            var hmac = new HMACSHA1();
            hmac.Key = Encoding.UTF8.GetBytes(signingKey);

            byte[] databuff = System.Text.Encoding.UTF8.GetBytes(result);
            byte[] hashbytes = hmac.ComputeHash(databuff);

            return Convert.ToBase64String(hashbytes);
        }

        private string GenerateNonce()
        {
            string nonce = string.Empty;
            var rand = new Random();
            int next = 0;
            for (var i = 0; i < 32; i++)
            {
                next = rand.Next(65, 90);
                char c = Convert.ToChar(next);
                nonce += c;
            }

            return nonce;
        }
    }
}