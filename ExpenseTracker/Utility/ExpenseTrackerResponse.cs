using ExpenseTracker.Utility.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracker.Utility
{
    public class ExpenseTrackerResponse
    {
        public ExpenseTrackerResponse(ResponseCode _statusCode)
        {
            this.StatusCode = _statusCode;
            this.StatusMessage = ExpenseTrackerResponse.getStatustextBasedOnStatusCode(_statusCode);
        }

        public ExpenseTrackerResponse(ResponseCode _statusCode, string _statusMessage)
        {
            this.StatusCode = _statusCode;
            this.StatusMessage = _statusMessage;
        }

        public ExpenseTrackerResponse()
        {
        }

        public bool IsRedirect { get; set; }
        public string RedirectUrl { get; set; }
        public object ResponseObject { get; set; }
        public ResponseCode StatusCode { get; set; }
        public string StatusDescription { get; set; }

        //  public string StatusMessage { get; set; }
        public string StatusMessage
        {
            get
            {
                return this._StatusMessage;
            }
            set
            {
                this._StatusMessage = ExpenseTrackerResponse.getStatustextBasedOnStatusCode(this.StatusCode);
            }
        }

        public static string getStatustextBasedOnStatusCode(ResponseCode _statusCode)
        {
            string _statusText = string.Empty;
            switch (_statusCode)
            {
                case ResponseCode.UserNotFound: _statusText = "UserId is not registered."; break;
                case ResponseCode.WrongUserIdOrPassword: _statusText = "Wrong UserId or Password."; break;
                case ResponseCode.ContactAdmin: _statusText = "Please contact admin."; break;
                default: _statusText = ""; break;
            }
            return _statusText;
        }

        private string _StatusMessage;
    }
}