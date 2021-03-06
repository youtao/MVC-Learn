﻿using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MVCLearn.ModelDbContext;
using StackExchange.Redis;

namespace MVCLearn.Service
{
    /// <summary>
    /// Service 基础类(非泛型)
    /// </summary>
    public abstract class BaseService
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
        /// <para>LearnDB数据库上下文(多线程版,Multi Thread)</para>
        /// <para>每次获取时重新再实例化一个,主要用于大量数据并发查询</para>
        /// </summary>
        protected LearnDbContext GetLearnDbMT()
        {
            return new LearnDbContext();
        }

        /// <summary>
        /// LearnDB数据库SqlConnection(每次获取重新实例化,没有打开连接)
        /// </summary>
        protected SqlConnection GetLearnDBConn()
        {
            var connStr = ConfigurationManager.ConnectionStrings["LearnDbContext"].ConnectionString;
            return new SqlConnection(connStr);
        }

        #endregion

        #region Mongo

        private IMongoDatabase _mongoDB;

        protected IMongoDatabase MongoDB
        {
            get
            {
                if (this._mongoDB == null)
                {
                    //todo:mongodb配置到web.config
                    var conn = ConfigurationManager.AppSettings["mongodb_conn"];
                    var client = new MongoClient(conn);
                    this._mongoDB = client.GetDatabase("MVCLearn");
                }
                return this._mongoDB;
            }
        }

        #endregion

        #region redis

        protected ConnectionMultiplexer GetRedisConn()
        {
            var server = ConfigurationManager.AppSettings["redis_server"];
            var port = ConfigurationManager.AppSettings["redis_port"];
            var password = ConfigurationManager.AppSettings["redis_password"];
            ConnectionMultiplexer conn = ConnectionMultiplexer.Connect($"{server}:{port},password={password}");
            return conn;
        }

        #endregion

        #endregion

        #region Http Cache DBContext

        /// <summary>
        /// 获取数据库上下文,缓存到当前HTTP请求上下文中
        /// </summary>
        /// <typeparam name="TAnyDbContext"></typeparam>
        protected TAnyDbContext GetDbContext<TAnyDbContext>()
            where TAnyDbContext : DbContext, new()
        {
            if (this.HttpContext == null)
            {
                throw new ArgumentException();
            }
            var key = "MVCLearn_" + typeof(TAnyDbContext).FullName;
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