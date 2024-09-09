using AutoMapper;
using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Entities;

namespace BookStoreAPIVer2;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Employee, EmployeeDTO>().ReverseMap();

        CreateMap<Employee, UpdateEmployeeDTO>().ReverseMap();
        
        CreateMap<Customer, CustomerDTO>().ReverseMap();

        CreateMap<Category, CategoryDTO>().ReverseMap();
    }
}