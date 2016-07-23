using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EntityFramework.Extensions;
using WebUI.Models;

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
                HasChildren = e.Children.Any()
            }).ToListAsync();
            return this.Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind(Include = "Title,Url,Icon,ParentId")] Menu entity)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                entity.Order = db.Menu.Count(e => e.ParentId == entity.ParentId);
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
            await db.Menu.Where(e => e.Id == id).UpdateAsync(e => new Menu()
            {
                Delete = true
            });
            return Json(true);
        }

        public async Task<JsonResult> Order([Bind(Include = "Id,Order,ParentId")]Menu param)
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
                    await db.Menu.Where(e => e.ParentId == param.ParentId && e.Order >= param.Order).UpdateAsync(e => new Menu()
                    {
                        Order = e.Order + 1 // 下面加1
                    });
                }
                else // 
                {
                    if (menu.Order < param.Order) // 从上往下移动
                    {
                        await db.Menu.Where(e =>
                        e.ParentId == param.ParentId &&
                        e.Order > menu.Order && // 不包含自己
                        e.Order <= param.Order) // 包含最下面被影响的
                        .UpdateAsync(e => new Menu()
                        {
                            Order = e.Order - 1 // 上面减1
                        });
                    }
                    else
                    {
                        await db.Menu.Where(e =>
                        e.ParentId == param.ParentId &&
                        e.Order >= param.Order && // 包含最上面被影响的
                        e.Order < menu.Order) // 不包含自己
                        .UpdateAsync(e => new Menu()
                        {
                            Order = e.Order + 1 // 下面加1
                        });
                    }
                }
                await db.Menu.Where(e => e.Id == param.Id).UpdateAsync(e => new Menu()
                {
                    Order = param.Order,
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
