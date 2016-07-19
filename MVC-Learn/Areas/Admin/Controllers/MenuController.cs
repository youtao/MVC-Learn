using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Learn.Models;

namespace MVC_Learn.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        private LearnDbContext db = new LearnDbContext();

        // GET: Admin/Menu
        public async Task<ActionResult> Index()
        {
            var menu = db.Menu.Include(m => m.Parent);
            return View(await menu.ToListAsync());
        }

        // GET: Admin/Menu/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menu.FindAsync(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Admin/Menu/Create
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.Menu, "Id", "Title");
            return View();
        }

        // POST: Admin/Menu/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Url,Icon,ParentId,CreateTime")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menu.Add(menu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.Menu, "Id", "Title", menu.ParentId);
            return View(menu);
        }

        // GET: Admin/Menu/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menu.FindAsync(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.Menu, "Id", "Title", menu.ParentId);
            return View(menu);
        }

        // POST: Admin/Menu/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Url,Icon,ParentId,CreateTime")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.Menu, "Id", "Title", menu.ParentId);
            return View(menu);
        }

        // GET: Admin/Menu/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menu.FindAsync(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Menu menu = await db.Menu.FindAsync(id);
            db.Menu.Remove(menu);
            await db.SaveChangesAsync();
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
