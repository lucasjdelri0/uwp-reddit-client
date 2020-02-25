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
    public class RedditService : IRedditService
    {
        private AuthInfo _authInfo;
        private HttpClient _httpClient;
        private DateTime _lastRequest;

        public RedditService(AuthInfo authInfo)
        {
            _authInfo = authInfo;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_authInfo.baseAddress);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(("application/json")));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_authInfo.tokenType, _authInfo.accessToken);
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("uwp-reddit-client", "v1"));
        }

        public async Task<List<Post>> GetTopPosts(int limit)
        {
            var url = $"/top?limit={limit}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                _lastRequest = DateTime.Now;

                var stringResponse = await response.Content.ReadAsStringAsync();

                List<Post> posts = GetPosts(stringResponse);

                return posts;
            }
            else
            {
                throw new HttpRequestException("[HttpRequestException]: GetTopPosts(): Request FAIL");
            }
        }

        private List<Post> GetPosts(string response)
        {
            //JsonConvert.DeserializeObject<List<PostResponseInfo>>(stringResponse);
            var myJson = JsonConvert.DeserializeObject<dynamic>(response);

            var postsList = new List<Post>();

            foreach (var post in myJson.data.children)
            {
                postsList.Add(
                    new Post(
                        _subreddit: post.data.subreddit,
                        _title: post.data.title,
                        _author: post.data.author,
                        _dtCreated: ConvertFromUnixTimestamp((int)post.data.created_utc),
                        _numberOfComments: post.data.num_comments)
                    );
            }

            return postsList;
        }

        private DateTime ConvertFromUnixTimestamp(int created)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(created);
        }
    }
}
