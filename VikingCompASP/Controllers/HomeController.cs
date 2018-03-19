using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VikingCompASP_Domain.Entities;
using VikingCompASP_Domain.Repository;
using PagedList;

namespace VikingCompASP.Controllers
{
    public class HomeController : Controller
    {
        INewsRepository newsRepository;
        IProductRepository productRepository;

        public HomeController(INewsRepository newsRepository, IProductRepository productRepository)
        {
            this.newsRepository = newsRepository;
            this.productRepository = productRepository;
        }


        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(newsRepository.SelectAll().OrderByDescending(x => x.Date).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Contact";

            return View();
        }

        public ActionResult Products(int? page)
        {
            ViewBag.Title = "Products";

            int pageSize = 9;
            int pageNumber = (page ?? 1);

            return View(productRepository.SelectAll().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ProdDetails(int id)
        {
            Product p = productRepository.GetByID(id);

            ViewBag.Title = p.Name + " details";

            return View(p);
        }

        public ActionResult NewsDetails(int id)
        {
            News n = newsRepository.GetByID(id);

            ViewBag.Title = n.Head;

            return View(n);
        }
    }
}