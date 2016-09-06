using AutoRecoveryServices.Email;
using ExpenseTracker.Models;
using ExpenseTracker.Utility;
using ExpenseTracker.Utility.Enumeration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ExpenseTracker.Controllers
{
    public class ExpenseTrackerController : BaseController
    {
        /// <summary>
        /// Dashboards this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Dashboard()
        {
            return View();
        }

        /// <summary>
        /// Gets the dashboard data.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public string GetDashboardData()
        {
            DashboardData dd = new DashboardData() { NoOfItems = 10, NoOfItemsText = "Items bought", TotalSpending = 1360, TotalSpendingText = "Total Spending", User = "Rajeev", UserText = "Welcome!", WishList = 2, WishListText = "may be this month" };
            return JsonConvert.SerializeObject(dd);
        }

        /// <summary>
        /// Gets the sign up code.
        /// </summary>
        /// <param name="User">The user.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public string GetSignUpCode(Users User)
        {
            try
            {
                var SignUpCode = GetRandomString(6).ToUpper();
                User.SignUpCode = SignUpCode;
                Session.Add("SignUpCode", SignUpCode);
                EmailManager.SendMail(User, MailType.SignUpCode);
                return "Email sent, Please check your email.";
            }
            catch (Exception)
            {
                return "Email sending failed, Please try again.";
            }
        }

        // GET: ExpenseTracker
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Logs the in.
        /// </summary>
        /// <returns></returns>
        public ActionResult LogIn()
        {
            return View();
        }

        /// <summary>
        /// Saves as json string.
        /// </summary>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public void SaveAsJsonString()
        {
            Users user = new Users() { Id = 1, FirstName = "Rajeev", LastName = "Ranjan", Email = "rajeevr@gmail.com", MonthlyLimit = new MonthlyExpenseLimit() { Highest = 300, Lowest = 100, Month = Month.April }, Password = "123", Setting = new UserSetting() { IsActive = true, IsAdmin = true, IsEnable = true } };
            List<Users> UserList = new List<Users>();
            UserList.Add(user);
            DataSerializer.JsonSerializerSaveAsFile<List<Users>>(UserList);
        }

        /// <summary>
        /// Signs the in.
        /// </summary>
        /// <param name="User">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// User not found;UserDefined
        /// or
        /// User not found;UserDefined
        /// </exception>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public string SignIn(Users User)
        {
            var UserPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//Users.Json");
            DataSerializer.JsonPath = UserPath;
            ExpenseTrackerResponse er = new ExpenseTrackerResponse();
            er.StatusCode = ResponseCode.UserNotFound;
            try
            {
                List<Users> UserList = DataSerializer.JsonDserializerFromFile<List<Users>>();
                if (UserList.Where(x => x.Email.ToLower() == User.Email.ToLower()).Count() > 0)
                {
                    var _getUserFromList = UserList.Where(x => x.Email.ToLower() == User.Email.ToLower() && x.Password == User.Password);
                    if (_getUserFromList.Count() > 0)
                    {
                        Session.Add("User", _getUserFromList.FirstOrDefault());
                        return DataSerializer.JsonSerializer<Users>(_getUserFromList.FirstOrDefault());
                    }
                    else
                    {
                        return DataSerializer.JsonSerializer<ExpenseTrackerResponse>(new ExpenseTrackerResponse(ResponseCode.WrongUserIdOrPassword));
                    }
                }
                else
                {
                    return DataSerializer.JsonSerializer<ExpenseTrackerResponse>(new ExpenseTrackerResponse(ResponseCode.UserNotFound));
                }
            }
            catch (Exception ex)
            {
                return DataSerializer.JsonSerializer<ExpenseTrackerResponse>(new ExpenseTrackerResponse() { StatusCode = ResponseCode.ContactAdmin, StatusMessage = ExpenseTrackerResponse.getStatustextBasedOnStatusCode(ResponseCode.ContactAdmin), StatusDescription = ex.Message.ToString() });
            }
        }

        /// <summary>
        /// Signs the in view.
        /// </summary>
        /// <returns></returns>
        public ActionResult SignInView()
        {
            return View();
        }

        /// <summary>
        /// Signs up.
        /// </summary>
        /// <param name="User">The user.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public string SignUp(Users User)
        {
            var UserPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//Users.Json");
            DataSerializer.JsonPath = UserPath;
            List<Users> UserList = null;
            if (System.IO.File.Exists(UserPath))
            {
                UserList = DataSerializer.JsonDserializerFromFile<List<Users>>();
                var FindUserInList = from d in UserList
                                     where d.Email == User.Email && d.Password == User.Password
                                     select d;
                if (FindUserInList.Count() > 0)
                {
                    return "Email Id already in use";
                }
                else
                {
                    if (Session["SignUpCode"] != null && User.SignUpCode == Convert.ToString(Session["SignUpCode"]))
                    {
                        UserList.Add(User);
                        RegisterUser(UserList);
                        EmailManager.SendMail(User, MailType.SignUp);
                        Session["User"] = User;
                        return "1";
                    }
                    else
                    {
                        return "Wrong SignUp Code.";
                    }
                }
            }
            else
            {
                if (Session["SignUpCode"] != null && User.SignUpCode == Convert.ToString(Session["SignUpCode"]))
                {
                    UserList = new List<Users>() { User };
                    RegisterUser(UserList);
                    EmailManager.SendMail(User, MailType.SignUp);
                    Session["User"] = User;
                    return "1";
                }
                else
                {
                    return "Wrong SignUp Code.";
                }
            }
        }

        /// <summary>
        /// Signs up view.
        /// </summary>
        /// <returns></returns>
        public ActionResult SignUpView()
        {
            return View();
        }

        //
        // Summary:
        //     Called when a request matches this controller, but no method with the specified
        //     action name is found in the controller.
        //
        // Parameters:
        //   actionName:
        //     The name of the attempted action.
        protected override void HandleUnknownAction(string actionName)
        {
            try
            {
                this.View(actionName).ExecuteResult(this.ControllerContext);
            }
            catch (Exception)
            {
                Response.Redirect("Error");
            }
        }

        /// <summary>
        /// Gets the random string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        private string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="UserList">The user list.</param>
        private void RegisterUser(List<Users> UserList)
        {
            DataSerializer.JsonSerializerSaveAsFile<List<Users>>(UserList);
        }
    }
}