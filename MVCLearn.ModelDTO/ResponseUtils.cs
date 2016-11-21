namespace MVCLearn.ModelDTO
{
    public class ResponseUtils
    {
        public static ResponseDto<TData> Converter<TData>(TData data, int state = 1, string message = "成功")
        {
            if (string.IsNullOrEmpty(message))
            {
                switch (state)
                {
                    case -1:
                        message = "服务器错误";
                        break;
                    case 0:
                        message = "失败";
                        break;
                    case 1:
                        message = "成功";
                        break;
                }
            }
            return new ResponseDto<TData>()
            {
                State = state.ToString(),
                Message = message,
                Data = data
            };
        }
    }
}