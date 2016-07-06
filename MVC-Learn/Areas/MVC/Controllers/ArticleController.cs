using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Learn.Models;

namespace MVC_Learn.Areas.MVC.Controllers
{
    public class ArticleController : Controller
    {
        private readonly LearnDbContext _db = new LearnDbContext();

        // GET: MVC/Article
        public ActionResult Index()
        {
            var list = _db.Article.OrderByDescending(e => e.Id).Take(20).Select(e => new
            {
                Uinque = e.Unique,
                e.Title,
                e.Author,
                e.Description,
                e.CreateTime
            })
             .ToList()
             .Select(e =>
             {
                 dynamic obj = new ExpandoObject();
                 obj.Unique = e.Uinque.ToString();
                 obj.Title = e.Title;
                 obj.Author = e.Author;
                 obj.Description = e.Description;
                 obj.CreateTime = e.CreateTime;
                 return obj;
             });
            return View(list);
        }

        // GET: MVC/Article/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await _db.Article.SingleOrDefaultAsync(e => e.Unique == id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: MVC/Article/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MVC/Article/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Author,Content,Description,Unique,CreateTime")] Article article)
        {
            if (ModelState.IsValid)
            {
                _db.Article.Add(article);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: MVC/Article/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await _db.Article.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: MVC/Article/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Author,Content,Description,Unique,CreateTime")] Article article)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(article).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: MVC/Article/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = await _db.Article.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: MVC/Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Article article = await _db.Article.FindAsync(id);
            _db.Article.Remove(article);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
