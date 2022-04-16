using eCommerceApp.Models;
using eCommerceApp.Repos;
using eCommerceApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace eCommerceApp.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly IShoppingCart ShoppingCart;
        private eCommerceAppEntities context;
        private eCommerceApp.Models.ApplicationUser userInfo;
        public CheckOutController( IShoppingCart _IShoppingCart, eCommerceAppEntities _context)
        {
            
            ShoppingCart = _IShoppingCart;
            context = _context;

        }
        // GET: CheckOut
        public ActionResult Index()
        {
            var items = ShoppingCart.GetShoppingCartItems();


            ShoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartViewModel()
            {
                ShoppingCart = (ShoppingCart)ShoppingCart,
                ShoppingCartTotal = ShoppingCart.ShoppingItemTotal(),
            };
            foreach(var item in context.Users.ToList())
            {
                if (item.UserName == @User.Identity.Name)
                {
                    userInfo = item;
                }      
            }
            var checkOut = new CheckOutViewModel()
            {
                s1 = response,
                userInfo = this.userInfo
            };
             return View(checkOut);
        }

        // GET: CheckOut/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CheckOut/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CheckOut/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CheckOut/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CheckOut/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CheckOut/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CheckOut/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
