using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ExpenseTracker.Models
{
    public enum pCategory
    {
        Kitchen = 1,
        Home = 2,
        Personal = 3,
        Clothing = 4,
        Others = 5
    }

    public class DashboardData
    {
        public int NoOfItems { get; set; }
        public string NoOfItemsText { get; set; }
        public int TotalSpending { get; set; }
        public string TotalSpendingText { get; set; }
        public string User { get; set; }
        public string UserText { get; set; }
        public int WishList { get; set; }
        public string WishListText { get; set; }
    }

    [DataContract(Name = "Product", Namespace = "")]
    public class Product
    {
        public Product()
        {
            this.CreatedDate = DateTime.Now;
            this.ModifiedDate = DateTime.Now;
        }

        [DataMember(Name = "Category", Order = 1)]
        public int Category { get; set; }

        [DataMember(Name = "CreatedDate", Order = 5)]
        public DateTime CreatedDate { get; set; }

        [DataMember(Name = "Id", Order = 2)]
        public int Id { get; set; }

        [DataMember(Name = "ModifiedDate", Order = 6)]
        public DateTime ModifiedDate { get; set; }

        [DataMember(Name = "Name", Order = 3)]
        public string Name { get; set; }

        [DataMember(Name = "Price", Order = 4)]
        public double Price { get; set; }

        [DataMember(Name = "UserID", Order = 0)]
        public int UserID { get; set; }
    }

    [DataContract(Name = "ProductCategory", Namespace = "")]
    public class ProductCategory
    {
        [DataMember(Name = "Id", Order = 0)]
        public int Id { get; set; }

        [DataMember(Name = "Name", Order = 1)]
        public string Name { get; set; }
    }
}