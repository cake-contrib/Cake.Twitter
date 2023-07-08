using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Twitter
{
    // The code within this TwitterProvider has been based almost exclusively on the work that was done by
    // Jamie Maguire in this repository
    // https://github.com/jamiemaguiredotnet/SocialOpinion-Public
    /// <summary>
    /// Contains functionality related to Twitter API
    /// </summary>
    public sealed class TwitterProvider
    {
        private const string Version = "1.0";
        private const string SignatureMethod = "HMAC-SHA1";
        private const string TwitterApiBaseUrl = "https://api.twitter.com/2/tweets";

        private readonly string _consumerKey;
        private readonly string _consumerKeySecret;
        private readonly string _accessToken;
        private readonly string _accessTokenSecret;
        private readonly IDictionary<string, string> _customParameters;

        /// <summary>
        /// Creates an object for sending tweets to Twitter using Single-user OAuth.
        ///
        /// Get your access keys by creating an app at apps.twitter.com then visiting the
        /// "Keys and Access Tokens" section for your app. They can be found under the
        /// "Your Access Token" heading.
        /// </summary>
        public TwitterProvider(string consumerKey, string consumerKeySecret, string accessToken, string accessTokenSecret)
        {
            _consumerKey = consumerKey;
            _consumerKeySecret = consumerKeySecret;
            _accessToken = accessToken;
            _accessTokenSecret = accessTokenSecret;
            _customParameters = new Dictionary<string, string>();
        }

        /// <summary>
        /// Sends a tweet with the supplied text and returns the response from the Twitter API.
        /// </summary>
        public Task<string> Tweet(string text)
        {
            return SendRequest(text);
        }

        private Task<string> SendRequest(string tweet)
        {
            var timespan = GetTimestamp();
            var nonce = CreateNonce();

            var parameters = new Dictionary<string, string>(_customParameters);
            AddOAuthParameters(parameters, timespan, nonce);

            var signature = GenerateSignature(parameters);
            var headerValue = GenerateAuthorizationHeaderValue(parameters, signature);

            var tweetContent = string.Format("{{\r\n    \"text\": \"{0}\"\r\n}}", tweet.Replace(Environment.NewLine, "\\r\\n"));
            var httpContent = new StringContent(tweetContent, Encoding.UTF8, "application/json");

            return SendRequest(headerValue, httpContent);
        }

        private async Task<string> SendRequest(string oAuthHeader, HttpContent httpContent)
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("Authorization", oAuthHeader);
                var httpResp = await http.PostAsync(TwitterApiBaseUrl, httpContent);
                var respBody = await httpResp.Content.ReadAsStringAsync();
                return respBody;
            }
        }

        private string GenerateSignature(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var dataToSign = new StringBuilder()
                .Append("POST")
                .Append("&")
                .Append(TwitterApiBaseUrl.EncodeDataString())
                .Append("&")
                .Append(parameters
                            .OrderBy(x => x.Key)
                            .Select(x => string.Format("{0}={1}", x.Key, x.Value))
                            .Join("&")
                            .EncodeDataString());

            var signatureKey = string.Format("{0}&{1}", _consumerKeySecret.EncodeDataString(), _accessTokenSecret.EncodeDataString());
            var sha1 = new HMACSHA1(Encoding.ASCII.GetBytes(signatureKey));

            var signatureBytes = sha1.ComputeHash(Encoding.ASCII.GetBytes(dataToSign.ToString()));
            return Convert.ToBase64String(signatureBytes);
        }

        private string GenerateAuthorizationHeaderValue(IEnumerable<KeyValuePair<string, string>> parameters, string signature)
        {
            return new StringBuilder("OAuth ")
                .Append(parameters.Concat(new KeyValuePair<string, string>("oauth_signature", signature))
                            .Where(x => x.Key.StartsWith("oauth_"))
                            .Select(x => string.Format("{0}=\"{1}\"", x.Key, x.Value.EncodeDataString()))
                            .Join(","))
                .ToString();
        }

        private void AddOAuthParameters(IDictionary<string, string> parameters, string timestamp, string nonce)
        {
            parameters.Add("oauth_version", Version);
            parameters.Add("oauth_consumer_key", _consumerKey);
            parameters.Add("oauth_nonce", nonce);
            parameters.Add("oauth_signature_method", SignatureMethod);
            parameters.Add("oauth_timestamp", timestamp);
            parameters.Add("oauth_token", _accessToken);
        }

        private static string GetTimestamp()
        {
            // Timestamps are in seconds since 1/1/1970.
            return ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }

        private static string CreateNonce()
        {
            return new Random().Next(0x0000000, 0x7fffffff).ToString("X8");
        }
    }

    public static class TwitterProviderExtensions
    {
        public static string Join<T>(this IEnumerable<T> items, string separator)
        {
            return string.Join(separator, items.ToArray());
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, T value)
        {
            return items.Concat(new[] { value });
        }

        public static string EncodeDataString(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return Uri.EscapeDataString(value);
        }
    }
}
