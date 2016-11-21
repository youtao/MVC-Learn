using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EntityFramework.Extensions;
using MVCLearn.Model;
using MVCLearn.ModelBCL;
using MVCLearn.ModelDbContext;

namespace MVCLearn.WebUI.Areas.Admin.Controllers
{
    public class MenuController : JsonNetController
    {
        private readonly LearnDbContext _db = new LearnDbContext();
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
            //Mapper.Initialize(e =>
            //e.CreateMap<Menu, MenuDto>()
            //.ForMember(dto => dto.Count,
            //conf => conf.MapFrom(src => src.Children.Count(ef => ef.Delete == false)))
            //);
            //var list = await _db.Menu.Where(e =>
            //        e.ParentID == parentId &&
            //        e.Delete == false)
            //    .ProjectTo<MenuDto>()
            //    .OrderBy(e => e.MenuOrder)
            //    .AsNoTracking()
            //    .ToListAsync();
            return this.Json(new object(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind(Include = "Title,Url,Icon,ParentID")] Menu entity)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                entity.MenuOrder = _db.Menu.Count(e => e.ParentID == entity.ParentID);
                _db.Menu.Add(entity);
                await _db.SaveChangesAsync();
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
                await _db.Menu.Where(e => e.ID == param.ID).UpdateAsync(e => new Menu()
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
            await _db.Menu.Where(e => e.ID == id).UpdateAsync(e => new Menu()
            {
                Delete = true
            });
            return Json(true);
        }

        public async Task<JsonResult> Order([Bind(Include = "Id,MenuOrder,ParentID")]Menu param)
        {
            var result = false;
            if (ModelState.IsValid)
            {
                var menu = _db.Menu.FirstOrDefault(e => e.ID == param.ID);
                if (menu == null)
                {
                    return Json(false);
                }

                if (menu.ParentID != param.ParentID) // 父Id改变了
                {
                    var list =
                        _db.Menu.Where(e => e.ParentID == param.ParentID && e.MenuOrder >= param.MenuOrder)
                            .Select(e => e.ID)
                            .ToList();
                    if (list.Count > 0)
                    {
                        await _db.Menu.Where(e => list.Contains(e.ID)).UpdateAsync(e => new Menu()
                        {
                            MenuOrder = e.MenuOrder + 1 // 下面加1
                        });
                    }
                }
                else
                {
                    if (menu.MenuOrder < param.MenuOrder) // 从上往下移动
                    {
                        var list = _db.Menu.Where(e =>
                            e.ParentID == param.ParentID &&
                            e.MenuOrder > menu.MenuOrder && // 不包含自己
                            e.MenuOrder <= param.MenuOrder) // 包含最下面被影响的
                            .Select(e => e.ID)
                            .ToList();
                        if (list.Count > 0)
                        {
                            await _db.Menu.Where(e => list.Contains(e.ID))
                            .UpdateAsync(e => new Menu()
                            {
                                MenuOrder = e.MenuOrder - 1 // 上面减1
                            });
                        }

                    }
                    else
                    {
                        var list = _db.Menu.Where(e =>
                            e.ParentID == param.ParentID &&
                            e.MenuOrder >= param.MenuOrder && // 包含最上面被影响的
                            e.MenuOrder < menu.MenuOrder)  // 不包含自己
                            .Select(e => e.ID)
                            .ToList();
                        if (list.Count > 0)
                        {
                            _db.Menu.Where(e => list.Contains(e.ID))
                            .Update(e => new Menu()
                            {
                                MenuOrder = e.MenuOrder + 1 // 下面加1
                            });
                        }

                    }
                }
                await _db.Menu.Where(e => e.ID == param.ID).UpdateAsync(e => new Menu()
                {
                    MenuOrder = param.MenuOrder,
                    ParentID = param.ParentID
                });
                result = true;
            }
            return Json(result);
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
