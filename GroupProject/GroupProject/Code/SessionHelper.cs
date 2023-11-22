using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupProject.Code
{
    public class SessionHelper
    {
        public static void SetSession(UserSession session)
        {
            HttpContext.Current.Session["UserSession"] = session;
        }
        public static UserSession GetUserSession()
        {
            var session = HttpContext.Current.Session["UserSession"];
            if (session == null)
            {
                return null;
            }
            else return session as UserSession;
        }
    }
}