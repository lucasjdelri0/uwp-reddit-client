using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPRedditClient.Entities;

namespace UWPRedditClient.ViewModels
{
    public class ItemViewModel
    {
        private int _postId;
        public int PostId
        {
            get
            {
                return _postId;
            }
        }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Subreddit { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string NumberOfComments { get; set; }
        public string Thumbnail { get; set; }
        public string MainPicture { get; set; }

        public ItemViewModel()
        {
        }

        public static ItemViewModel FromPostToItemViewModel(Post post)
        {
            var itemViewModel = new ItemViewModel();

            itemViewModel._postId = post.id;
            itemViewModel.Subreddit = post.subreddit;
            itemViewModel.Title = post.title;
            itemViewModel.Author = post.author;
            itemViewModel.DateTimeCreated = post.dateTimeCreated;
            itemViewModel.NumberOfComments = post.numberOfComments;
            itemViewModel.Thumbnail = post.thumbnail;
            itemViewModel.MainPicture = post.mainPicture;

            return itemViewModel;
        }
    }
}
