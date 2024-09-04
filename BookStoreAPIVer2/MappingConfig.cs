using AutoMapper;
using BookStoreAPIVer2.DTOs;
using BookStoreAPIVer2.Entities;

namespace BookStoreAPIVer2;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<NhanVien, NhanVienDTO>().ReverseMap();
        
        CreateMap<KhachHang, KhachHangDTO>().ReverseMap();
    }
}