using AutoMapper;

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

        }
    }
}