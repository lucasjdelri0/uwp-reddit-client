using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPRedditClient.Entities;

namespace UWPRedditClient.Interfaces
{
    public interface IRedditService
    {
        Task<List<Post>> GetTopPosts(int limit);
    }
}
