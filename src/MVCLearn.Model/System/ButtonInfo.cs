using System.ComponentModel.DataAnnotations.Schema;
using MVCLearn.ModelBCL;
using MVCLearn.ModelEnum;

namespace MVCLearn.Model
{
    /// <summary>
    /// 用户Model
    /// </summary>
    [Table("System_ButtonInfo")]
    public class ButtonInfo : IntBaseModel
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