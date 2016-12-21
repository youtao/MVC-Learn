namespace MVCLearn.ModelEnum
{
    public enum AuthorizeState
    {
        /// <summary>
        /// 默认
        /// </summary>
        没有登录 = 0,

        /// <summary>
        /// 传来了认证,但没通过
        /// </summary>
        认证失败 = 1,

        /// <summary>
        /// 认证成功了,但没有权限访问
        /// </summary>
        没有权限 = 2,
    }
}