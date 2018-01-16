using AutoMapper;
using PTApi.Data.Models;
using PTApi.ViewModels.Mahasiswa;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Mahasiswa, MhsVm>()
            .ForMember(mv => mv.Nim, opt => opt.MapFrom(m => m.Nim))
            .ReverseMap();

        CreateMap<Mahasiswa, MhsDetVm>()
            .ForMember(mv => mv.Nim, opt => opt.MapFrom(m => m.Nim))
            .ReverseMap();

        CreateMap<MhsKls, MhsKlsVm>();
        CreateMap<Kelas, MhsKlsDetVm>();
    }
}