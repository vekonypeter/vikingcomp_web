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
    [Authorize]
    public class OrderController : Controller
    {
        IOrderRepository orderRepository;
        IProductRepository productRepository;

        public OrderController(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
        }

        public ActionResult Index(int? page)
        {
            IEnumerable<Order> orders = orderRepository.GetByUserID(UserRepository.GetUserIdByName(User.Identity.Name));
            int unPaid = orders.Count(o => !o.Paid);

            int pageSize = 9;
            int pageNumber = (page ?? 1);

            ViewBag.Title = "My orders";
            if (unPaid != 0)
            {
                int debit = orders.Where(o => !o.Paid).Sum(o => o.Product.Price_Net);
                ViewBag.Message = "You have " + unPaid + " unpaid order(s)! Your full debit is: " + debit + " Ft (net)";
            }
            return View(orders.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create(int prodId, string msg)
        {
            Product p = productRepository.GetByID(prodId);

            ViewBag.Title = "New order of " + p.Name;

            return View(new Order() {
                UserId = UserRepository.GetUserIdByName(User.Identity.Name),
                ProductId = prodId,
                Date = DateTime.Now,
                Paid = false,
                Completed = false,
                Product = productRepository.GetByID(prodId)
            });
        }

        [HttpPost]
        public ActionResult Create(Order o)
        {
            try
            {
                Product p = productRepository.GetByID(o.ProductId);

                if (o.Quantity > p.Quantity || o.Quantity < 1)
                {
                    return RedirectToAction("Create", new { prodId = o.ProductId, msg = "You can't order less than one or more than the available quantity!" });
                }

                p.Quantity -= o.Quantity;
                productRepository.Change(p);
                orderRepository.Add(o);

                return RedirectToAction("Details", new { oId = o.Id, msg = "You have successfully ordered " + o.Quantity + " pcs of " + p.Name + "(s) !" });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int oId, string msg)
        {
            ViewBag.Title = "Order details";

            ViewBag.Message = msg;

            Order o = orderRepository.GetByID(oId);

            return View(o);
        }

        public ActionResult Pay(int oId)
        {
            Order o = orderRepository.GetByID(oId);
            o.Paid = true;
            orderRepository.Change(o);

            return RedirectToAction("Details", new { oId = o.Id, msg = "You have successfully paid for your order!" });
        }
    }
}
