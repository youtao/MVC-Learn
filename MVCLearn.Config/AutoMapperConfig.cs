using AutoMapper;
using MVCLearn.Model;
using MVCLearn.ModelDTO;

namespace MVCLearn.Config
{
    public class AutoMapperConfig
    {
        public static void MapperInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DefaultProfile>();
            });
        }
    }

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