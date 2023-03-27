using AutoMapper;

using Shared.DTO;

namespace Application.Mappers;
public class MappingProfile : Profile 
{ 
    public MappingProfile() 
    { 
        CreateMap<Entities.Models.Customer, CustomerDto>().ReverseMap();
        CreateMap<CustomerForCreationDto, Entities.Models.Customer>().ReverseMap();
        CreateMap<CustomerForUpdateDto, Entities.Models.Customer>().ReverseMap();
    } 
}