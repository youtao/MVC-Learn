using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace MVCLearn.ModelDTO.Privilege
{
    /// <summary>
    /// 权限DTO
    /// </summary>
    public class PrivilegeDTO
    {
        [JsonIgnore]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 访问权限
        /// </summary>
        /// <value>The accesses.</value>
        public List<AccessInfoDTO> Accesses { get; set; }

        /// <summary>
        /// 菜单权限
        /// </summary>
        /// <value>The menus.</value>
        public List<MenuInfoDTO> Menus { get; set; }

        /// <summary>
        /// 按钮权限
        /// </summary>
        /// <value>The buttons.</value>
        public List<ButtonInfoDTO> Buttons { get; set; }
    }
}