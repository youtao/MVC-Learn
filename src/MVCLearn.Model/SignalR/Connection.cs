using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MVCLearn.ModelBCL;

namespace MVCLearn.Model
{
    /// <summary>
    /// SignalR连接信息Model
    /// </summary>
    [Table("SignalR_Connection")]
    public class Connection : IntBaseModel
    {
        public Connection()
        {
            this.Connected = true;
        }

        /// <summary>
        /// 连接Id
        /// </summary>
        [Required]
        public string ConnectionId { get; set; }

        /// <summary>
        /// 浏览器标识
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 是否连接
        /// </summary>
        public bool Connected { get; set; }

        #region 导航属性

        /// <summary>
        /// 用户ID(外键)
        /// </summary>
        [ForeignKey("UserInfo")]
        public int UserInfoID { get; set; }
        /// <summary>
        /// 用户(导航)
        /// </summary>
        public virtual UserInfo UserInfo { get; set; }

        #endregion

    }
    // todo:连接页面,连接时间...
}