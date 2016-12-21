using MVCLearn.ModelEnum;

namespace MVCLearn.ModelDTO
{
    public class ResponseUtils
    {
        public static ResponseDTO<TData> Converter<TData>(TData data, ResponseState state = ResponseState.成功, string message = "")
        {
            if (string.IsNullOrEmpty(message))
            {
                switch (state)
                {
                    case ResponseState.失败:
                        message = "失败";
                        break;
                    case ResponseState.成功:
                        message = "成功";
                        break;
                    case ResponseState.未登录:
                        message = "未登录";
                        break;
                    case ResponseState.权限不足:
                        message = "权限不足";
                        break;
                    case ResponseState.服务器错误:
                        message = "服务器错误";
                        break;
                }
            }
            return new ResponseDTO<TData>()
            {
                State = (int)state + string.Empty,
                Message = message,
                Data = data
            };
        }
    }
}