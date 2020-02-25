using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPRedditClient.Entities
{
    public class AuthInfo
    {
        public string accessToken { get; set; }
        public string tokenType { get; set; }
        public DateTime expiresIn { get; set; }
        public string baseAddress { get; set; }
    }
}
