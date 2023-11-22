using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Code
{
    [Serializable]
    public class UserSession
    {
        private string username { get; set; }
        public UserSession(string username)
        {
            this.username = username;
        }
        public string getUserName()
        {
            return username;
        }
    }
}