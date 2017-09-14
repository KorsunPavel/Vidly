using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start {
    public class MappingProfile : Profile {
        public MappingProfile() {
            Mapper.CreateMap<Customer, CustomerDto>().ForMember(m => m.id, opt => opt.Ignore());
            Mapper.CreateMap<CustomerDto, Customer>();

            Mapper.CreateMap<Movie, MovieDto>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<MovieDto, Movie>();

            Mapper.CreateMap<Rental, NewRenatlDto>();
            Mapper.CreateMap<NewRenatlDto, Rental>();
        }
    }
}