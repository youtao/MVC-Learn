using AutoMapper;
using MVCLearn.Model;
using MVCLearn.ModelDTO;

namespace MVCLearn.Config
{
    /// <summary>
    ///  AutoMapper Config.
    /// </summary>
    public class AutoMapperConfig
    {
        /// <summary>
        /// 初始化 AutoMapper Config.
        /// </summary>
        public static void MapperInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DefaultProfile>();
            });
        }
    }
    /// <summary>
    /// 默认 AutoMapper Profile.
    /// </summary>
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            // model->dto this.CreateMap<model, dto>();
            this.CreateMap<UserInfo, UserInfoDTO>() // UserInfo-->UserInfoDTO
                .ForMember(dto => dto.UserID, conf => conf.MapFrom(model => model.ID));
        }
    }
}