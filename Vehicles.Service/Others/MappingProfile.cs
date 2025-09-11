using AutoMapper;
using Vehicles.Models;

namespace Vehicles.Others
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleMake, VehicleMakeDTO>()
                .ForMember(make => make.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(make => make.Abrv, opt => opt.MapFrom(src => src.Abrv))
				.ForMember(make => make.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<VehicleMakeDTO, VehicleMake>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Abrv, opt => opt.MapFrom(src => src.Abrv))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Model, opt => opt.Ignore());

            CreateMap<VehicleModel, VehicleModelDTO>()
            .ForMember(model => model.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(model => model.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(model => model.Abrv, opt => opt.MapFrom(src => src.Abrv))
            .ForMember(dest => dest.MakeAbrv, opt => opt.MapFrom(src => src.Make.Abrv))
            .ForMember(dest => dest.MakeName, opt => opt.MapFrom(src => src.Make.Name))
            .ForMember(model => model.FullName, opt => opt.MapFrom(src => src.Make.Abrv + " - " + src.Name))
            .ForMember(dest => dest.MakeId, opt => opt.MapFrom(src => src.MakeId));

			CreateMap<VehicleModelDTO, VehicleModel>()
			.ForMember(model => model.Id, opt => opt.MapFrom(src => src.Id))
			.ForMember(model => model.Name, opt => opt.MapFrom(src => src.Name))
			.ForMember(model => model.Abrv, opt => opt.MapFrom(src => src.Abrv))
            .ForMember(dest => dest.MakeId, opt => opt.MapFrom(src => src.MakeId))
            .ForMember(model => model.Make, opt => opt.Ignore());
		}
    }
}
