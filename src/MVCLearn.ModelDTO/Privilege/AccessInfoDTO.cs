namespace MVCLearn.ModelDTO.Privilege
{
    /// <summary>
    /// 访问权限 DTO.
    /// </summary>
    public class AccessInfoDTO
    {
        /// <summary>
        /// 菜单ID.
        /// </summary>
        public int AccessID { get; set; }

        /// <summary>
        /// 标题.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 地址.
        /// </summary>
        public string Url { get; set; }
    }
}