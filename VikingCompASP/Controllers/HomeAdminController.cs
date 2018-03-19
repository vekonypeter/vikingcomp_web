using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VikingCompASP_Domain.Entities;
using VikingCompASP_Domain.Repository;

namespace VikingCompASP.Controllers
{
    [Authorize(Roles="admin")]
    public class HomeAdminController : Controller
    {
        IProductRepository productRepository;
        IProdCategoryRepository prodCategoryRepository;
        INewsRepository newsRepository;
        IOrderRepository orderRepository;
        IUserRepository userRepository;

        public HomeAdminController(IProductRepository productRepository, IProdCategoryRepository prodCategoryRepository, INewsRepository newsRepository, IOrderRepository orderRepository, IUserRepository userRepository)
        {
            this.prodCategoryRepository = prodCategoryRepository;
            this.productRepository = productRepository;
            this.newsRepository = newsRepository;
            this.orderRepository = orderRepository;
            this.userRepository = userRepository;
        }



        //
        // GENERAL
        //

        public ActionResult Index()
        {
            ViewBag.Title = "Admin management";

            return View();
        }



        //
        // NEWS
        //

        public ActionResult NewsIndex()
        {
            ViewBag.Title = "Manage News";

            return View(newsRepository.SelectAll());
        }

        public ActionResult NewsDetails(int id)
        {
            ViewBag.Title = "New Details";

            return View(newsRepository.GetByID(id));
        }

        public ActionResult NewsCreate()
        {
            ViewBag.Title = "Create New";

            return View(new News() { Date = DateTime.Now });
        }

        [HttpPost]
        public ActionResult NewsCreate(News n)
        {
            try
            {
                newsRepository.Add(n);

                return RedirectToAction("NewsIndex");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult NewsEdit(int id)
        {
            ViewBag.Title = "Edit New";

            return View(newsRepository.GetByID(id));
        }

        [HttpPost]
        public ActionResult NewsEdit(News edited)
        {
            try
            {
                newsRepository.Change(edited);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult NewsDelete(int id)
        {
            ViewBag.Title = "Delete New";

            return View(newsRepository.GetByID(id));
        }

        [HttpPost]
        public ActionResult NewsDelete(int id, FormCollection collection)
        {
            try
            {
                newsRepository.Delete(newsRepository.GetByID(id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        //
        // PRODUCTS
        //

        public ActionResult ProdIndex()
        {
            ViewBag.Title = "Manage Products";

            return View(productRepository.SelectAll());
        }
        
        public ActionResult ProdsInIncOrdersReport()
        {
            ViewBag.Title = "Report: products in incomlete orders";

            return View(productRepository.GetProdsInIncOrders());
        }
        
        public ActionResult ProdDetails(int id)
        {
            Product p = productRepository.GetByID(id);

            ViewBag.Title = p.Name + " details";

            return View(p);
        }

        public ActionResult ProdCreate()
        {
            ViewBag.Title = "Create Product";

            return View(new Product());
        }

        [HttpPost]
        public ActionResult ProdCreate(Product p)
        {
            try
            {
                productRepository.Add(p);

                return RedirectToAction("ProdIndex");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ProdEdit(int id)
        {
            Product p = productRepository.GetByID(id);

            ViewBag.Title = "Edit " + p.Name;

            return View(p);
        }

        [HttpPost]
        public ActionResult ProdEdit(Product edited)
        {
            try
            {
                productRepository.Change(edited);

                return RedirectToAction("ProdIndex");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ProdDelete(int id)
        {
            Product p = productRepository.GetByID(id);

            ViewBag.Title = "Delete " + p.Name;

            return View(p);
        }

        [HttpPost]
        public ActionResult ProdDelete(int id, FormCollection collection)
        {
            try
            {
                productRepository.Delete(productRepository.GetByID(id));

                return RedirectToAction("ProdIndex");
            }
            catch
            {
                return View();
            }
        }



        //
        // PRODUCT CATEGORIES
        //

        public ActionResult ProdCatIndex()
        {
            ViewBag.Title = "Manage Product Categories";

            return View(prodCategoryRepository.SelectAll());
        }

        public ActionResult ProdCatProdReport()
        {
            ViewBag.Title = "Report: Products by Categories";

            return View(prodCategoryRepository.GetProdsByProdCat());
        }

        public ActionResult ProdCatDetails(int id)
        {
            ProdCategory pc = prodCategoryRepository.GetByID(id);

            ViewBag.Title = pc.Name + " details";

            return View(pc);
        }

        public ActionResult ProdCatCreate()
        {
            ViewBag.Title = "Create Product Category";

            return View(new ProdCategory());
        }

        [HttpPost]
        public ActionResult ProdCatCreate(ProdCategory p)
        {
            try
            {
                prodCategoryRepository.Add(p);

                return RedirectToAction("ProdCatIndex");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ProdCatEdit(int id)
        {
            ProdCategory pc = prodCategoryRepository.GetByID(id);

            ViewBag.Title = "Edit " + pc.Name;

            return View(pc);
        }

        [HttpPost]
        public ActionResult ProdCatEdit(ProdCategory edited)
        {
            try
            {
                prodCategoryRepository.Change(edited);

                return RedirectToAction("ProdCatIndex");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ProdCatDelete(int id)
        {
            ProdCategory pc = prodCategoryRepository.GetByID(id);

            ViewBag.Title = "Delete " + pc.Name;

            return View(pc);
        }

        [HttpPost]
        public ActionResult ProdCatDelete(int id, FormCollection collection)
        {
            try
            {
                prodCategoryRepository.Delete(prodCategoryRepository.GetByID(id));

                return RedirectToAction("ProdCatIndex");
            }
            catch
            {
                return View();
            }
        }




        //
        // ORDERS
        //

        public ActionResult OrderIndex()
        {
            ViewBag.Title = "Manage Orders";

            return View(orderRepository.SelectAll());
        }

        public ActionResult OrderOverviewReport()
        {
            ViewBag.Title = "Report: order overview";

            return View(orderRepository.GetOrderOverview());
        }

        public ActionResult OrderDetails(int id)
        {
            ViewBag.Title = "Order Details";

            return View(orderRepository.GetByID(id));
        }

        public ActionResult OrderCreate()
        {
            ViewBag.Title = "Create Order";

            return View(new Order());
        }

        [HttpPost]
        public ActionResult OrderCreate(Order p)
        {
            try
            {
                orderRepository.Add(p);

                return RedirectToAction("OrderIndex");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult OrderEdit(int id)
        {
            ViewBag.Title = "Edit Order";

            return View(orderRepository.GetByID(id));
        }

        [HttpPost]
        public ActionResult OrderEdit(Order edited)
        {
            try
            {
                Order old = orderRepository.GetByID(edited.Id);
                Product p = productRepository.GetByID(edited.ProductId);
                p.Quantity += old.Quantity - edited.Quantity;
                productRepository.Change(p);

                orderRepository.Change(edited);

                return RedirectToAction("OrderIndex");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult OrderDelete(int id)
        {
            ViewBag.Title = "Delete Order";

            return View(orderRepository.GetByID(id));
        }

        [HttpPost]
        public ActionResult OrderDelete(int id, FormCollection collection)
        {
            try
            {
                Order o = orderRepository.GetByID(id);

                if (!o.Completed)
                {
                    Product p = productRepository.GetByID(o.ProductId);
                    p.Quantity += o.Quantity;
                    productRepository.Change(p);
                }
                orderRepository.Delete(o);

                return RedirectToAction("OrderIndex");
            }
            catch
            {
                return View();
            }
        }





        //
        // USERS
        //

        public ActionResult UserIndex()
        {
            ViewBag.Title = "Manage Users";

            return View(userRepository.SelectAll());
        }

        public ActionResult UserOrderReport()
        {
            ViewBag.Title = "Report: User orders";

            return View(userRepository.GetOrdersByUsers());
        }

        public ActionResult UserDetails(string id)
        {
            AspNetUsers u = userRepository.GetByID(id);

            ViewBag.Title = u.UserName + " details";

            return View(u);
        }

        public ActionResult UserEdit(string id)
        {
            AspNetUsers u = userRepository.GetByID(id);

            ViewBag.Title = "Edit " + u.UserName;

            return View(u);
        }

        [HttpPost]
        public ActionResult UserEdit(AspNetUsers edited)
        {
            try
            {
                userRepository.Change(edited);

                return RedirectToAction("UserIndex");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UserDelete(string id)
        {
            AspNetUsers u = userRepository.GetByID(id);

            ViewBag.Title = "Delete " + u.UserName;

            return View(u);
        }

        [HttpPost]
        public ActionResult UserDelete(string id, FormCollection collection)
        {
            try
            {
                userRepository.Delete(userRepository.GetByID(id));

                return RedirectToAction("UserIndex");
            }
            catch
            {
                return View();
            }
        }

    }
}
