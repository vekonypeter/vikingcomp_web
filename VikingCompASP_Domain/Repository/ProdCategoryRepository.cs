using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VikingCompASP_Domain.Entities;

namespace VikingCompASP_Domain.Repository
{
    public interface IProdCategoryRepository
    {
        IEnumerable<ProdCategory> SelectAll();
        ProdCategory GetByID(int Id);
        void Add(ProdCategory pc);
        void Delete(ProdCategory pc);
        void Change(ProdCategory pc);

        IEnumerable<ProdsByProdCatVM> GetProdsByProdCat();
    }

    public class ProdCategoryRepository : IProdCategoryRepository
    {
        VikingCompModel vikingcompContext;

        public ProdCategoryRepository()
        {
            vikingcompContext = new VikingCompModel();
        }

        public IEnumerable<ProdCategory> SelectAll()
        {
            return vikingcompContext.ProdCategory.ToList();
        }

        public ProdCategory GetByID(int Id)
        {
            return vikingcompContext.ProdCategory.Find(Id);
        }

        public void Add(ProdCategory n)
        {
            vikingcompContext.ProdCategory.Add(n);
            vikingcompContext.SaveChanges();
        }

        public void Delete(ProdCategory n)
        {
            vikingcompContext.ProdCategory.Remove(n);
            vikingcompContext.SaveChanges();
        }

        public void Change(ProdCategory pc)
        {
            vikingcompContext.ProdCategory.Find(pc.Id).Description = pc.Description;
            vikingcompContext.ProdCategory.Find(pc.Id).Name = pc.Name;

            vikingcompContext.SaveChanges();
        }

        public IEnumerable<ProdsByProdCatVM> GetProdsByProdCat()
        {
            var data =
                    from pc in vikingcompContext.ProdCategory
                    join sub in vikingcompContext.Product.GroupBy(x => x.ProdCategory) on pc equals sub.Key into gj
                    from subpc in gj.DefaultIfEmpty()
                    select new ProdsByProdCatVM()
                    {
                        ProdCatName = pc.Name,
                        ProdCount = subpc.Count(),
                        TotalQuantity = subpc.Sum(i => i.Quantity),
                        TotalPrice = subpc.Sum(i => i.Price_Net * i.Quantity)
                    };

            return data.ToList();
        }




        public static List<SelectListItem> GetAllToSelectList()
        {
            VikingCompModel cntx = new VikingCompModel();

            var all = cntx.ProdCategory;
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in all)
            {
                list.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
            }
            return list;
        }
    }
}
