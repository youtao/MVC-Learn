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
    public abstract class BaseService<TCurrentDbContext, TEntity>
        where TCurrentDbContext : DbContext, new()
        where TEntity : BaseModel, new()
    {
        #region constructor

        protected BaseService() { }

        protected BaseService(HttpContextBase httpContext)
        {
            this.HttpContext = httpContext;
        }

        #endregion

        #region property

        /// <summary>
        /// 当前请求HTTP上下文
        /// </summary>
        protected HttpContextBase HttpContext { get; }

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

        #region LearnDB

        /// <summary>
        /// LearnDB数据库上下文
        /// </summary>
        private LearnDbContext _learnDB;
        /// <summary>
        /// LearnDB数据库上下文
        /// </summary>
        protected LearnDbContext LearnDB
        {
            get
            {
                if (this.HttpContext != null)
                {
                    return this.GetDbContext<LearnDbContext>();
                }
                else
                {
                    return this._learnDB ?? (this._learnDB = new LearnDbContext());
                }
            }
        }

        /// <summary>
        /// <para>LearnDB数据库上下文(多线程版,Multi Thread).</para>
        /// <para>每次获取时重新再实例化一个,主要用于大量数据并发查询.</para>
        /// </summary>
        protected LearnDbContext GetLearnDbMT()
        {
            return new LearnDbContext();
        }

        /// <summary>
        /// LearnDB数据库SqlConnection.(每次获取重新实例化,没有打开连接)
        /// </summary>
        protected SqlConnection GetLearnDBConn()
        {
            var connStr = ConfigurationManager.ConnectionStrings["LearnDbContext"].ConnectionString;
            return new SqlConnection(connStr);
        }

        #endregion

        #endregion

        #region protected method

        /// <summary>
        /// 所有没被软删除的数据(当前实体模型).
        /// </summary>
        /// <returns></returns>
        protected IQueryable<TEntity> AllNotDelete()
        {
            return this.CurrentDB.Set<TEntity>().Where(e => e.Delete == false);
        }

        #endregion

        #region private method

        /// <summary>
        /// 获取数据库上下文,缓存到当前HTTP请求上下文中
        /// </summary>
        /// <typeparam name="TAnyDbContext"></typeparam>
        /// <returns></returns>
        private TAnyDbContext GetDbContext<TAnyDbContext>()
            where TAnyDbContext : DbContext, new()
        {
            if (this.HttpContext == null)
            {
                throw new ArgumentException();
            }
            var key = typeof(TAnyDbContext).FullName + "__HttpContext";
            var dbContext = this.HttpContext.Items[key] as TAnyDbContext;
            if (dbContext == null)
            {
                dbContext = new TAnyDbContext();
                this.HttpContext.Items.Add(key, dbContext);
            }
            return dbContext;
        }

        #endregion
    }
}