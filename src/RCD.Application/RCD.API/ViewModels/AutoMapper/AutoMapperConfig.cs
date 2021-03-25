using AutoMapper;
using RCD.Domain.Models;

namespace RCD.API.ViewModels.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Customer, BaseCustomerViewModel>()
                .ForMember(dest => dest.Email, mo => mo.MapFrom(src => src.Email.EmailAddress));
            CreateMap<BaseCustomerViewModel, Customer>();

            CreateMap<Customer, CustomerViewModel>()
                .ForMember(dest => dest.Email, mo => mo.MapFrom(src => src.Email.EmailAddress));
            CreateMap<CustomerViewModel, Customer>();

            CreateMap<Phone, PhoneViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
        }
    }
}
