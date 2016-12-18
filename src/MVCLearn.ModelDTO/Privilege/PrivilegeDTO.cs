using System.Collections.Generic;
using System.Runtime.Serialization;
using MongoDB.Bson;

namespace MVCLearn.ModelDTO.Privilege
{
    public class PrivilegeDTO
    {
        public ObjectId Id { get; set; }

        public int UserID { get; set; }
        /// <summary>
        /// 访问权限.
        /// </summary>
        /// <value>The accesses.</value>
        public List<AccessInfoDTO> Accesses { get; set; }
        /// <summary>
        /// 按钮权限.
        /// </summary>
        /// <value>The buttons.</value>
        public List<ButtonInfoDTO> Buttons { get; set; }
        /// <summary>
        /// 菜单权限.
        /// </summary>
        /// <value>The menus.</value>
        public List<MenuInfoDTO> Menus { get; set; }
    }
}