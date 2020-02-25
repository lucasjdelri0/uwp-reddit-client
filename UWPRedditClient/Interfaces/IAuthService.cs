using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPRedditClient.Entities;

namespace UWPRedditClient.Interfaces
{
    public interface IAuthService
    {
        Task<AuthInfo> GetAuthInfo(string username, string password, string client_id, string client_secret);
    }
}
