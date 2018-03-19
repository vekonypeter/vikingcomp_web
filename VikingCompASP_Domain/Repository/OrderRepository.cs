using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VikingCompASP_Domain.Entities;

namespace VikingCompASP_Domain.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> SelectAll();
        Order GetByID(int Id);
        IEnumerable<Order> GetByUserID(string Id);
        IEnumerable<Order> GetByProdID(int Id);
        void Add(Order o);
        void Delete(Order o);
        void Change(Order o);
        
        OrderOverviewVM GetOrderOverview();
    }
    public class OrderRepository : IOrderRepository
    {
        VikingCompModel vikingcompContext;

        public OrderRepository()
        {
            vikingcompContext = new VikingCompModel();
        }

        public IEnumerable<Order> SelectAll()
        {
            return vikingcompContext.Order.ToList();
        }

        public Order GetByID(int Id)
        {
            return vikingcompContext.Order.Find(Id);
        }

        public void Add(Order n)
        {
            vikingcompContext.Order.Add(n);
            vikingcompContext.SaveChanges();
        }

        public void Delete(Order n)
        {
            vikingcompContext.Order.Remove(n);
            vikingcompContext.SaveChanges();
        }

        public IEnumerable<Order> GetByUserID(string Id)
        {
            return vikingcompContext.Order.Where(x => x.UserId == Id).ToList();
        }

        public IEnumerable<Order> GetByProdID(int Id)
        {
            return vikingcompContext.Order.Where(x => x.ProductId == Id).ToList();
        }


        public void Change(Order o)
        {
            vikingcompContext.Order.Find(o.Id).Completed = o.Completed;
            vikingcompContext.Order.Find(o.Id).Date = o.Date;
            vikingcompContext.Order.Find(o.Id).Paid = o.Paid;
            vikingcompContext.Order.Find(o.Id).Quantity = o.Quantity;

            vikingcompContext.SaveChanges();
        }

        public OrderOverviewVM GetOrderOverview()
        {
            return new OrderOverviewVM()
            {
                OrderCnt = vikingcompContext.Order.Count(),
                InCompCnt = vikingcompContext.Order.Count(x => !x.Completed),
                TotalClaim = vikingcompContext.Order.Where(x => !x.Paid).Sum(x => x.Product.Price_Net * x.Quantity),
                TotalIncome = vikingcompContext.Order.Where(x => x.Paid).Sum(x => x.Product.Price_Net * x.Quantity),
                TotalQuantity = vikingcompContext.Order.Sum(x => x.Quantity),
                UnPaidCnt = vikingcompContext.Order.Count(x => !x.Paid)
            };           
        }
    }
}
