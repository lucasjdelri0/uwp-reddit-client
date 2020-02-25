using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UWPRedditClient.Entities;
using UWPRedditClient.Interfaces;

namespace UWPRedditClient.Services
{
    public class AuthService : IAuthService
    {
        private static volatile AuthService instance;
        private static object syncRoot = new Object();

        private AuthInfo authInfo;
        private static readonly string REDDIT_AUTH_URL = "https://www.reddit.com/api/v1/access_token";
        private static readonly string REDDIT_BASE_URL = "https://oauth.reddit.com";

        public AuthService()
        {
        }

        public static IAuthService GetAuthenticationService()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new AuthService();
                    }
                }
            }

            return instance;
        }

        public async Task<AuthInfo> Authenticate(string username, string password, string client_id, string client_secret)
        {
            try
            {
                AuthResponseInfo response = await GetToken(username, password, client_id, client_secret);

                authInfo = new AuthInfo()
                {
                    accessToken = response.access_token,
                    tokenType = response.token_type,
                    expiresIn = DateTime.Now.AddSeconds(response.expires_in),
                    baseAddress = REDDIT_BASE_URL
                };

                return authInfo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new AuthInfo();
            }
        }

        private async Task<AuthResponseInfo> GetToken(string username, string password, string client_id, string client_secret)
        {
            var url = $"{REDDIT_AUTH_URL}?grant_type=password&username={username}&password={password}";

            var byteArray = Encoding.ASCII.GetBytes($"{client_id}:{client_secret}");

            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(("application/json")));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("basic", Convert.ToBase64String(byteArray));

            var response = await client.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                var myJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AuthResponseInfo>(myJson);
            }
            else
            {
                throw new HttpRequestException("Error requesting authentication token. Check your credentials.");
            }

        }

    }
}
