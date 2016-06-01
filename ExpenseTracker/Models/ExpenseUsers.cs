using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ExpenseTracker.Models
{
    public enum Month
    {
        NotSet = 0,
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    [DataContract(Name = "MonthlyExpenseLimit", Namespace = "")]
    public class MonthlyExpenseLimit
    {
        [DataMember(Name = "Highest", Order = 2)]
        public double Highest { get; set; }

        [DataMember(Name = "Lowest", Order = 1)]
        public double Lowest { get; set; }

        // [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Name = "Month", Order = 0)]
        public Month Month { get; set; }
    }

    [DataContract(Name = "Userlogs", Namespace = "")]
    public class Userlogs
    {
        public Userlogs()
        {
            this.WhereIm = new List<WhereIm>();
        }

        [DataMember(Name = "Id", Order = 0)]
        public int Id { get; set; }

        [DataMember(Name = "LogIn", Order = 1)]
        public DateTime LogIn { get; set; }

        [DataMember(Name = "LogOut", Order = 2)]
        public DateTime LogOut { get; set; }

        [DataMember(Name = "Logs", Order = 3)]
        public List<WhereIm> WhereIm { get; set; }
    }

    [DataContract(Name = "Users", Namespace = "")]
    public class Users
    {
        [DataMember(Name = "Email", Order = 3)]
        public string Email { get; set; }

        [DataMember(Name = "FirstName", Order = 1)]
        public string FirstName { get; set; }

        [DataMember(Name = "Id", Order = 0)]
        public int Id { get; set; }

        [DataMember(Name = "LastName", Order = 2)]
        public string LastName { get; set; }

        [DataMember(Name = "MonthlyLimit", Order = 6)]
        public MonthlyExpenseLimit MonthlyLimit { get; set; }

        [DataMember(Name = "Password", Order = 4)]
        public string Password { get; set; }

        [DataMember(Name = "PhoneNumber", Order = 5)]
        public Int16 PhoneNumber { get; set; }

        [DataMember(Name = "Setting", Order = 6)]
        public UserSetting Setting { get; set; }
    }

    [DataContract(Name = "UserSetting", Namespace = "")]
    public class UserSetting
    {
        public UserSetting()
        {
            this.CreatedDate = DateTime.Now;
            this.ModifiedDate = DateTime.Now;
        }

        [DataMember(Name = "IsActive", Order = 0)]
        public bool IsActive { get; set; }

        [DataMember(Name = "IsAdmin", Order = 1)]
        public bool IsAdmin { get; set; }

        [DataMember(Name = "IsEnable", Order = 2)]
        public bool IsEnable { get; set; }

        [DataMember(Name = "ModifiedDate", Order = 4)]
        public DateTime ModifiedDate { get; set; }

        [DataMember(Name = "CreatedDate", Order = 3)]
        private DateTime CreatedDate { get; set; }
    }

    [DataContract(Name = "ModuleAccess", Namespace = "")]
    public class WhereIm
    {
        [DataMember(Name = "CheckInTime", Order = 0)]
        public DateTime CheckInTime { get; set; }

        [DataMember(Name = "Content", Order = 1)]
        public string Content { get; set; }
    }
}