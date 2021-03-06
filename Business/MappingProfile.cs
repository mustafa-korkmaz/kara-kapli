﻿
using AutoMapper;

namespace Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AllowNullCollections = true;

            // Add as many of these lines as you need to map your objects
            CreateMap<Dto.DtoBase, Dal.Entities.EntityBase>();
            CreateMap<Dal.Entities.EntityBase, Dto.DtoBase>();

            CreateMap<Dal.Entities.Identity.ApplicationUser, Dto.User.ApplicationUser>()
                  .ForMember(dest => dest.MembershipExpiresAt,
                  opt => opt.MapFrom(src => src.LockoutEnd.HasValue ? src.LockoutEnd.Value.DateTime : new System.DateTime()))
                  .ForMember(dest => dest.IsSocialLogin, opt => opt.MapFrom(src => src.LockoutEnabled));
            CreateMap<Dto.User.ApplicationUser, Dal.Entities.Identity.ApplicationUser>()
                .ForMember(dest => dest.LockoutEnd, opt => opt.MapFrom(src => src.MembershipExpiresAt))
                .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => src.IsSocialLogin));
         
            CreateMap<Dto.Parameter, Dal.Entities.Parameter>();
            CreateMap<Dal.Entities.Parameter, Dto.Parameter>();

            CreateMap<Dto.ParameterType, Dal.Entities.ParameterType>();
            CreateMap<Dal.Entities.ParameterType, Dto.ParameterType>();

            CreateMap<Dto.Customer, Dal.Entities.Customer>();
            CreateMap<Dal.Entities.Customer, Dto.Customer>();

            CreateMap<Dto.Transaction, Dal.Entities.Transaction>();
            CreateMap<Dal.Entities.Transaction, Dto.Transaction>();

            CreateMap<Dto.Feedback, Dal.Entities.Feedback>();
            CreateMap<Dal.Entities.Feedback, Dto.Feedback>();

            CreateMap<Dal.Entities.Dashboard, Dto.Dashboard>();

            CreateMap<Dto.File, Dal.Entities.File>();
            CreateMap<Dal.Entities.File, Dto.File>();
        }
    }
}