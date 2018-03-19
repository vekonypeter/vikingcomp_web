using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VikingCompASP_Domain.Entities;

namespace VikingCompASP_Domain.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> SelectAll();
        Product GetByID(int Id);
        void Add(Product p);
        void Delete(Product p);
        void Change(Product p);

        IEnumerable<ProdsInIncOrdersVM> GetProdsInIncOrders();
    }

    public class ProductRepository : IProductRepository
    {

        VikingCompModel vikingcompContext;

        public ProductRepository()
        {
            vikingcompContext = new VikingCompModel();
        }

        public IEnumerable<Product> SelectAll()
        {
            return vikingcompContext.Product.ToList();
        }

        public Product GetByID(int Id)
        {
            return vikingcompContext.Product.Find(Id);
        }

        public void Add(Product n)
        {
            vikingcompContext.Product.Add(n);
            vikingcompContext.SaveChanges();
        }

        public void Delete(Product n)
        {
            vikingcompContext.Product.Remove(n);
            vikingcompContext.SaveChanges();
        }

        public void Change(Product p)
        {
            vikingcompContext.Product.Find(p.Id).CategoryId = p.CategoryId;
            vikingcompContext.Product.Find(p.Id).Description = p.Description;
            vikingcompContext.Product.Find(p.Id).Name = p.Name;
            vikingcompContext.Product.Find(p.Id).Price_Net = p.Price_Net;
            vikingcompContext.Product.Find(p.Id).Quantity = p.Quantity;

            vikingcompContext.SaveChanges();
        }

        public IEnumerable<ProdsInIncOrdersVM> GetProdsInIncOrders()
        {
            var data = from p in vikingcompContext.Product
                       join sub in (vikingcompContext.Order.Where(x => !x.Completed).GroupBy(x => x.Product)) on p equals sub.Key into gj
                       from subp in gj.DefaultIfEmpty()
                       select new ProdsInIncOrdersVM
                       {
                           ProdName = p.Name,
                           ProdCnt = subp.Sum(x => x.Quantity),
                           OrderCnt = subp.Count(),
                           OrderPaidCnt = subp.Count(x => x.Paid),
                           OrderPriceSum = subp.Sum(x => x.Product.Price_Net * x.Quantity),
                           OrderPaidPriceSum = subp.Where(x => x.Paid).Sum(x => x.Product.Price_Net * x.Quantity)
                       };
            return data;
        }
    }
}
