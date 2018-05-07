using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecKevinKeizoKASP.ClassHelper;
using RecKevinKeizoKASP.Models;

namespace RecKevinKeizoKASP.Controllers
{
    public class NewsController : Controller
    {
        private DataAccessObject db = new DataAccessObject();

        // GET: News
        public ActionResult Index(string Pesquisa = "")
        {
            var q = db.News.AsQueryable();
            if (!string.IsNullOrEmpty(Pesquisa))
                q = q.Where(c => c.Category.Name.Contains(Pesquisa));
            q = q.OrderBy(c => c.Category.Name);
            if (Request.IsAjaxRequest())
                return PartialView("_News", q.ToList());
            var news = db.News.Include(n => n.Category);
            return View(q.ToList());
        }

        // GET: News/Details/5
        public ActionResult Details(int? id, string Pesquisa = "")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categorias = db.Categories.OrderByDescending(a => a.CategoryId).ToList();
            var b = db.News.AsQueryable();
            if (!string.IsNullOrEmpty(Pesquisa))
                b = b.Where(c => c.Category.Name.Contains(Pesquisa));
            b = b.OrderBy(c => c.Category.Name);
            if (Request.IsAjaxRequest())
                return PartialView("_News", b.ToList());

            return View(news);
        }
        [Authorize]
        // GET: News/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: News/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                db.News.Add(news);
                db.SaveChanges();
                if (news.LogoFile != null)
                {
                    var pic = string.Empty;
                    var folder = "~/Photos";
                    var file = string.Format("{0}.png", news.NewsId);
                    var response = FilesHelper.UploadPhoto(news.LogoFile, folder, file);
                    if (response)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        news.Photo = pic;

                    }

                }

                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");


            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", news.CategoryId);
            return View(news);
        }
        [Authorize]
        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", news.CategoryId);
            return View(news);
        }

        // POST: News/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsId,Title,CategoryId,PostDate,Photo,Capa,Text,ResumeText,Author")] News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", news.CategoryId);
            return View(news);
        }
        [Authorize]

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}