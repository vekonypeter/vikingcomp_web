using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VikingCompASP_Domain.Entities;

namespace VikingCompASP_Domain.Repository
{
    public interface INewsRepository
    {
        IEnumerable<News> SelectAll();
        News GetByID(int Id);
        void Add(News n);
        void Delete(News n);
        void Change(News n);
    }
    public class NewsRepository : INewsRepository
    {
        VikingCompModel vikingcompContext;
        
        public NewsRepository()
        {
            vikingcompContext = new VikingCompModel();
        }

        public IEnumerable<News> SelectAll()
        {
            return vikingcompContext.New.ToList();
        }

        public News GetByID(int Id)
        {
            return vikingcompContext.New.Find(Id);
        }

        public void Add(News n)
        {
            vikingcompContext.New.Add(n);
            vikingcompContext.SaveChanges();
        }

        public void Delete(News n)
        {
            vikingcompContext.New.Remove(n);
            vikingcompContext.SaveChanges();
        }

        public void Change(News n)
        {
            vikingcompContext.New.Find(n.Id).Body = n.Body;
            vikingcompContext.New.Find(n.Id).Head = n.Head;
            vikingcompContext.New.Find(n.Id).Date = n.Date;

            vikingcompContext.SaveChanges();
        }
    }
}
