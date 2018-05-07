using RecKevinKeizoKASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecKevinKeizoKASP.Controllers
{
    public class HomeController : Controller
    {
        private DataAccessObject db = new DataAccessObject();
        public ActionResult Index()
        {
            ViewBag.Categorias = db.Categories.OrderByDescending(a => a.CategoryId).ToList();
            return View(db.News.OrderByDescending(a => a.NewsId).Take(5).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}