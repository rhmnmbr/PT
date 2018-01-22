using AutoMapper;
using PTApi.Data.Models;
using PTApi.ViewModels;
using PTApi.ViewModels.Dosen;
using PTApi.ViewModels.Kelas;
using PTApi.ViewModels.Mahasiswa;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Mahasiswa, MhsVm>()
            .ForMember(mv => mv.Nim, opt => opt.MapFrom(m => m.Nim)).ReverseMap();
        CreateMap<Mahasiswa, MhsDetVm>()
            .ForMember(mv => mv.Nim, opt => opt.MapFrom(m => m.Nim)).ReverseMap();
        CreateMap<MhsKls, MhsKlsVm>();
        CreateMap<Kelas, MhsKlsDetVm>();

        CreateMap<Kelas, KlsVm>()
            .ForMember(mv => mv.KodeKelas, opt => opt.MapFrom(k => k.KodeKelas)).ReverseMap();
        CreateMap<Kelas, KlsDetVm>()
            .ForMember(mv => mv.KodeKelas, opt => opt.MapFrom(k => k.KodeKelas)).ReverseMap();
        CreateMap<Kelas, KlsDsnPutVm>()
            .ForMember(mv => mv.KodeKelas, opt => opt.MapFrom(k => k.KodeKelas)).ReverseMap();
        CreateMap<MhsKls, KlsMhsVm>();
        CreateMap<Mahasiswa, KlsMhsDetVm>();
        CreateMap<Dosen, KlsDsnVm>();
        CreateMap<Dosen, KlsDsnPutDetVm>();

        CreateMap<Dosen, DsnVm>()
            .ForMember(mv => mv.NIK, opt => opt.MapFrom(d => d.NIK)).ReverseMap();
        CreateMap<Dosen, DsnDetVm>()
            .ForMember(mv => mv.NIK, opt => opt.MapFrom(d => d.NIK)).ReverseMap();
        CreateMap<Kelas, DsnKlsVm>();

        CreateMap<MhsKls, MKVm>()
            .ForMember(mv => mv.KodeKelas, opt => opt.MapFrom(k => k.KodeKelas)).ReverseMap()
            .ForMember(mv => mv.Nim, opt => opt.MapFrom(k => k.Nim)).ReverseMap();
        CreateMap<MhsKls, MKNilVm>()
            .ForMember(mv => mv.KodeKelas, opt => opt.MapFrom(k => k.KodeKelas)).ReverseMap()
            .ForMember(mv => mv.Nim, opt => opt.MapFrom(k => k.Nim)).ReverseMap();
    }
}