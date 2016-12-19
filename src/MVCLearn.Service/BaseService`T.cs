using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MVCLearn.ModelBCL;
using MVCLearn.ModelDbContext;

namespace MVCLearn.Service
{
    /// <summary>
    /// Service 基础类(泛型)
    /// </summary>
    /// <typeparam name="TCurrentDbContext">当前实体模型所在的数据库上下文</typeparam>
    /// <typeparam name="TEntity">当前的实体类型</typeparam>
    public abstract class BaseService<TCurrentDbContext, TEntity> : BaseService
        where TCurrentDbContext : DbContext, new()
        where TEntity : BaseModel, new()
    {
        #region constructor

        protected BaseService()
        {
        }

        protected BaseService(HttpContextBase httpContext) : base(httpContext)
        {
        }

        #endregion

        #region CurrentDB
        /// <summary>
        /// 当前实体模型数据库上下文
        /// </summary>
        private TCurrentDbContext _currentDB;
        /// <summary>
        /// 当前实体模型数据库上下文
        /// </summary>
        protected TCurrentDbContext CurrentDB
        {
            get
            {
                if (this.HttpContext != null)
                {
                    return this.GetDbContext<TCurrentDbContext>();
                }
                else
                {
                    return this._currentDB ?? (this._currentDB = new TCurrentDbContext());
                }
            }
        }

        #endregion

        #region protected method

        /// <summary>
        /// 所有没被软删除的数据(当前实体模型)
        /// </summary>
        /// <returns></returns>
        protected IQueryable<TEntity> AllNotDelete()
        {
            return this.CurrentDB.Set<TEntity>().Where(e => e.Delete == false);
        }

        #endregion

    }
}