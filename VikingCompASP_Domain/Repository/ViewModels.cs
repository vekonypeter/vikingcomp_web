using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VikingCompASP_Domain.Repository
{
    //
    // PRODUCT REPORT
    //
    public class ProdsInIncOrdersVM
    {
        [DisplayName("Product Name")]
        public string ProdName { get; set; }
        [DisplayName("Quantity")]
        public int? ProdCnt { get; set; }
        [DisplayName("Orders cnt.")]
        public int? OrderCnt { get; set; }
        [DisplayName("Paid orders cnt.")]
        public int? OrderPaidCnt { get; set; }
        [DisplayName("Full")]
        [DataType(DataType.Currency)]
        public int? OrderPriceSum { get; set; }
        [DisplayName("Paid")]
        [DataType(DataType.Currency)]
        public int? OrderPaidPriceSum { get; set; }
    }

    //
    // PRODUCT CATEGORY REPORT
    //
    public class ProdsByProdCatVM
    {
        [DisplayName("Category Name")]
        public string ProdCatName { get; set; }
        [DisplayName("Products cnt.")]
        public int? ProdCount { get; set; }
        [DisplayName("Total Quantity")]
        public int? TotalQuantity { get; set; }        
        [DisplayName("Total Price")]
        [DataType(DataType.Currency)]
        public int? TotalPrice { get; set; }
    }

    //
    // USERS REPORT
    //
    public class OrdersByUsersVM
    {
        [DisplayName("User")]
        public string UserName { get; set; }
        [DisplayName("Last Order")]
        public DateTime? LastOrder { get; set; }
        [DisplayName("Order cnt.")]
        public int? OrderCount { get; set; }
        [DisplayName("Completed cnt.")]
        public int? CompOrderCount { get; set; }
        [DisplayName("Paid cnt.")]
        public int? PaidOrderCount { get; set; }
        [DisplayName("Total Paid")]
        [DataType(DataType.Currency)]
        public int? TotalPaid { get; set; }
        [DisplayName("Total Debit")]
        [DataType(DataType.Currency)]
        public int? TotalDebit { get; set; }
    }

    //
    // ORDERS REPORT
    //
    public class OrderOverviewVM
    {
        [DisplayName("Order cnt.")]
        public int? OrderCnt { get; set; }
        [DisplayName("Unpaid")]
        public int? UnPaidCnt { get; set; }
        [DisplayName("Incomplete")]
        public int? InCompCnt { get; set; }
        [DataType(DataType.Currency)]
        [DisplayName("Total Income")]
        public int? TotalIncome { get; set; }
        [DataType(DataType.Currency)]
        [DisplayName("Total Claim")]
        public int? TotalClaim { get; set; }
        [DisplayName("Total Quantity")]
        public int? TotalQuantity { get; set; }
    }
}
