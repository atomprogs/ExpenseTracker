using ExpenseTracker.Models;
using ExpenseTracker.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTracker.Controllers
{
    public class ExpenseTrackerController : BaseController
    {
        public ActionResult Dashboard()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public string GetDashboardData()
        {
            DashboardData dd = new DashboardData() { NoOfItems = 10, NoOfItemsText = "Items bought", TotalSpending = 1360, TotalSpendingText = "Total Spending", User = "Rajeev", UserText = "Welcome!", WishList = 2, WishListText = "may be this month" };
            return JsonConvert.SerializeObject(dd);
        }

        // GET: ExpenseTracker
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public void SaveAsJsonString()
        {
            Users user = new Users() { Id = 1, FirstName = "Rajeev", LastName = "Ranjan", Email = "rajeevr@gmail.com", MonthlyLimit = new MonthlyExpenseLimit() { Highest = 300, Lowest = 100, Month = Month.April }, Password = "123", Setting = new UserSetting() { IsActive = true, IsAdmin = true, IsEnable = true } };
            List<Users> UserList = new List<Users>();
            UserList.Add(user);
            DataSerializer.JsonSerializerSaveAsFile<List<Users>>(UserList);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public string SignIn(Users User)
        {
            var UserPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//Users.Json");
            DataSerializer.JsonPath = UserPath;
            try
            {
                List<Users> UserList = DataSerializer.JsonDserializerFromFile<List<Users>>();
                var FindUserInList = from d in UserList
                                     where d.Email == User.Email && d.Password == User.Password
                                     select d;
                if (FindUserInList.Count() > 0)
                {
                    Session.Add("User", FindUserInList);
                    return DataSerializer.JsonSerializer<Users>(FindUserInList);
                }
                else
                {
                    throw new System.ArgumentException("User not found", "UserDefined");
                }
            }
            catch (Exception)
            {
                throw new System.ArgumentException("User not found", "UserDefined");
            }
        }

        public ActionResult SignInView()
        {
            return View();
        }

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
                    UserList.Add(User);
                    RegisterUser(UserList);
                    return "1";
                }
            }
            else
            {
                UserList = new List<Users>() { User };
                RegisterUser(UserList);
                return "1";
            }
        }

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

        private void RegisterUser(List<Users> UserList)
        {
            DataSerializer.JsonSerializerSaveAsFile<List<Users>>(UserList);
        }
    }
}