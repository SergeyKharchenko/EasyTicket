using EasyTicket.Api.Dto;
using EasyTicket.SharedResources.Models;
using EasyTicket.SharedResources.Models.Tables;

namespace EasyTicket.Api.Infrastructure {
    public static class Mapper {
        static Mapper() {
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<RequestDto, Request>().ReverseMap();
                cfg.CreateMap<ReservationDto, Reservation>().ReverseMap();
            });
        }

        public static TDestination Map<TSource, TDestination>(TSource source) {
            return AutoMapper.Mapper.Map<TSource, TDestination>(source);
        }
    }
}