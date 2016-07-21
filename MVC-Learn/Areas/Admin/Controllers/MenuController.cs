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
using EntityFramework.Extensions;

namespace MVC_Learn.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        private LearnDbContext db = new LearnDbContext();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 根据父键获取菜单
        /// </summary>
        /// <param name="parentId">父键Id</param>
        /// <returns></returns>
        public async Task<JsonResult> GetMenu(long? parentId = null)
        {
            var json = await db.Menu.Where(e =>
            e.ParentId == parentId &&
            e.Delete == false)
            .Select(e => new
            {
                e.Id,
                e.Title,
                e.Url,
                e.Icon,
                HasChildren = e.Children.Any()
            }).ToListAsync();
            return this.Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "Title,Url,Icon,ParentId")] Menu menu)
        {
            var menuId = -1L;
            if (ModelState.IsValid)
            {
                db.Menu.Add(menu);
                db.SaveChanges();
                menuId = menu.Id;
            }
            return Json(new { MenuId = menuId });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit([Bind(Include = "Id,Title,Url,Icon")] Menu param)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                result = true;
                await db.Menu.Where(e => e.Id == param.Id).UpdateAsync(e => new Menu()
                {
                    Title = param.Title,
                    Url = param.Url,
                    Icon = param.Icon
                });
            }
            return this.Json(result);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Delete(long id)
        {
            var result = false;
            result = await db.Menu.Where(e => e.Id == id).UpdateAsync(e => new Menu()
            {

            }) > 0; // TODO:软删除
            return Json("");
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
