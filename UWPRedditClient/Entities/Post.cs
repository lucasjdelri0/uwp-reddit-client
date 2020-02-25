using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace UWPRedditClient.Entities
{
    public class Post
    {
        public string subreddit { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public DateTime dateTimeCreated { get; set; }
        public int numberOfComments { get; set; }

        public Post(string _subreddit, string _title, string _author, DateTime _dtCreated, int _numberOfComments)
        {
            subreddit = _subreddit;
            title = _title;
            author = _author;
            dateTimeCreated = _dtCreated;
            numberOfComments = _numberOfComments;
        }

        public Post()
        {
        }
    }
}
