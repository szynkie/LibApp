using AutoMapper;
using LibApp.Dtos;
using LibApp.Models;

namespace LibApp.Profiles
{
    public class RentalProfile : Profile
    {
        public RentalProfile()
        {
            CreateMap<Rental, RentalDto>();
            CreateMap<RentalDto, Rental>();
        }
    }
}