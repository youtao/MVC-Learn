using MVCLearn.ModelEnum;

namespace MVCLearn.ModelDTO
{
    /// <summary>
    /// 按钮DTO
    /// </summary>
    public class ButtonInfoDTO
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string ButtonName { get; set; }

        /// <summary>
        /// 按钮类型
        /// </summary>
        public ButtonType ButtonType { get; set; }
    }
}