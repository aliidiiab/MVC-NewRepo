using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Controllers
{
    public class ProceedController : Controller
    {
        // GET: ProceedController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProceedController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProceedController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProceedController/Create
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

        // GET: ProceedController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProceedController/Edit/5
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

        // GET: ProceedController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProceedController/Delete/5
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
