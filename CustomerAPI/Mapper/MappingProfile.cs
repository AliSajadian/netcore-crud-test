using AutoMapper;

using Shared.DTO;

public class MappingProfile : Profile 
{ 
    public MappingProfile() 
    { 
        CreateMap<Entities.Models.Customer, CustomerDto>();
        CreateMap<CustomerForCreationDto, Entities.Models.Customer>();
        CreateMap<CustomerForUpdateDto, Entities.Models.Customer>();
    } 
}