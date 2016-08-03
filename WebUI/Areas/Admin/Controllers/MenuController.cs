using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EntityFramework.Extensions;
using Model;

namespace WebUI.Areas.Admin.Controllers
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
                e.Children.Count,
                MenuOrder = e.MenuOrder
            })
            .OrderBy(e => e.MenuOrder)
            .ToListAsync();
            return this.Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind(Include = "Title,Url,Icon,ParentId")] Menu entity)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                entity.MenuOrder = db.Menu.Count(e => e.ParentId == entity.ParentId);
                db.Menu.Add(entity);
                await db.SaveChangesAsync();
                result = true;
            }
            return Json(result);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit([Bind(Include = "Id,Title,Url,Icon")] Menu param)
        {
            var result = false;
            if (ModelState.IsValid)
            {                
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
            await db.Menu.Where(e => e.Id == id).UpdateAsync(e => new Menu()
            {
                Delete = true
            });
            return Json(true);
        }

        public async Task<JsonResult> Order([Bind(Include = "Id,MenuOrder,ParentId")]Menu param)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                var menu = db.Menu.FirstOrDefault(e => e.Id == param.Id);
                if (menu == null)
                {
                    return Json(false);
                }

                if (menu.ParentId != param.ParentId) // 父Id改变了
                {
                    var list =
                        db.Menu.Where(e => e.ParentId == param.ParentId && e.MenuOrder >= param.MenuOrder)
                            .Select(e => e.Id)
                            .ToList();
                    if (list.Count > 0)
                    {
                        await db.Menu.Where(e => list.Contains(e.Id)).UpdateAsync(e => new Menu()
                        {
                            MenuOrder = e.MenuOrder + 1 // 下面加1
                        });
                    }
                }
                else
                {
                    if (menu.MenuOrder < param.MenuOrder) // 从上往下移动
                    {
                        var list = db.Menu.Where(e =>
                            e.ParentId == param.ParentId &&
                            e.MenuOrder > menu.MenuOrder && // 不包含自己
                            e.MenuOrder <= param.MenuOrder) // 包含最下面被影响的
                            .Select(e => e.Id)
                            .ToList();
                        if (list.Count > 0)
                        {
                            await db.Menu.Where(e => list.Contains(e.Id))
                            .UpdateAsync(e => new Menu()
                            {
                                MenuOrder = e.MenuOrder - 1 // 上面减1
                            });
                        }

                    }
                    else
                    {
                        var list = db.Menu.Where(e =>
                            e.ParentId == param.ParentId &&
                            e.MenuOrder >= param.MenuOrder && // 包含最上面被影响的
                            e.MenuOrder < menu.MenuOrder)  // 不包含自己
                            .Select(e => e.Id)
                            .ToList();
                        if (list.Count > 0)
                        {
                            db.Menu.Where(e => list.Contains(e.Id))
                            .Update(e => new Menu()
                            {
                                MenuOrder = e.MenuOrder + 1 // 下面加1
                            });
                        }

                    }
                }
                await db.Menu.Where(e => e.Id == param.Id).UpdateAsync(e => new Menu()
                {
                    MenuOrder = param.MenuOrder,
                    ParentId = param.ParentId
                });
                result = true;
            }
            return Json(result);
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
