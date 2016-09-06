using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracker.Utility.Enumeration
{
    public enum MailType
    {
        SignUpCode = 1,
        SignUp = 2,
        Notification = 3,
    }

    public enum ResponseCode
    {
        UserNotFound = 601,
        WrongUserIdOrPassword = 602,
        ContactAdmin = 603,
    }
}