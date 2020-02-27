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
        public int id { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string subreddit { get; set; }
        public DateTime dateTimeCreated { get; set; }
        public string numberOfComments { get; set; }
        public string thumbnail { get; set; }
        public string mainPicture { get; set; }
    }
}
