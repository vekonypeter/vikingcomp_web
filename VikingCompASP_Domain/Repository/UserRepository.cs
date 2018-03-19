using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VikingCompASP_Domain.Entities;

namespace VikingCompASP_Domain.Repository
{
    public interface IUserRepository
    {
        IEnumerable<AspNetUsers> SelectAll();
        AspNetUsers GetByID(string Id);
        AspNetUsers GetByName(string name);
        void Add(AspNetUsers u);
        void Delete(AspNetUsers u);
        void Change(AspNetUsers u);

        IEnumerable<OrdersByUsersVM> GetOrdersByUsers();
    }

    public class UserRepository : IUserRepository
    {
        VikingCompModel vikingcompContext;
        
        public UserRepository()
        {
            vikingcompContext = new VikingCompModel();
        }

        public IEnumerable<AspNetUsers> SelectAll()
        {
            return vikingcompContext.AspNetUsers.ToList();
        }

        public AspNetUsers GetByID(string Id)
        {
            return vikingcompContext.AspNetUsers.Find(Id);
        }

        public AspNetUsers GetByName(string name)
        {
            return vikingcompContext.AspNetUsers.Where(u => u.UserName == name).First();
        }

        public void Add(AspNetUsers u)
        {
            vikingcompContext.AspNetUsers.Add(u);
            vikingcompContext.SaveChanges();
        }

        public void Delete(AspNetUsers u)
        {
            vikingcompContext.AspNetUsers.Remove(u);
            vikingcompContext.SaveChanges();
        }

        public void Change(AspNetUsers u)
        {
            vikingcompContext.AspNetUsers.Find(u.Id).UserName = u.UserName;
            vikingcompContext.AspNetUsers.Find(u.Id).TwoFactorEnabled = u.TwoFactorEnabled;
            vikingcompContext.AspNetUsers.Find(u.Id).SecurityStamp = u.SecurityStamp;
            vikingcompContext.AspNetUsers.Find(u.Id).PhoneNumber = u.PhoneNumber;
            vikingcompContext.AspNetUsers.Find(u.Id).PhoneNumberConfirmed = u.PhoneNumberConfirmed;
            vikingcompContext.AspNetUsers.Find(u.Id).LockoutEndDateUtc = u.LockoutEndDateUtc;
            vikingcompContext.AspNetUsers.Find(u.Id).LockoutEnabled = u.LockoutEnabled;
            vikingcompContext.AspNetUsers.Find(u.Id).Email = u.Email;
            vikingcompContext.AspNetUsers.Find(u.Id).EmailConfirmed = u.EmailConfirmed;
            vikingcompContext.AspNetUsers.Find(u.Id).AccessFailedCount = u.AccessFailedCount;

            vikingcompContext.SaveChanges();
        }

        public IEnumerable<OrdersByUsersVM> GetOrdersByUsers()
        {
            var data = from u in vikingcompContext.AspNetUsers
                       join sub in vikingcompContext.Order.GroupBy(x => x.AspNetUsers) on u equals sub.Key into gj
                       from subu in gj.DefaultIfEmpty()
                       select new OrdersByUsersVM()
                       {
                           UserName = u.UserName,
                           LastOrder = subu.OrderBy(x => x.Date).FirstOrDefault().Date,
                           OrderCount = subu.Count(),
                           CompOrderCount = subu.Count(x => x.Completed),
                           PaidOrderCount = subu.Count(x => x.Paid),
                           TotalDebit = subu.Where(x => !x.Paid).Sum(x => x.Product.Price_Net * x.Quantity),
                           TotalPaid = subu.Sum(x => x.Product.Price_Net * x.Quantity)
                       };

            return data.ToList();
        }




        //
        // STATIC
        //

        public static string GetUserIdByName(string name)
        {
            VikingCompModel cntx = new VikingCompModel();
            var user = cntx.AspNetUsers.Where(x => x.UserName == name).First();

            return user.Id;
        }
    }
}
