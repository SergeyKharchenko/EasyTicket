using AutoMapper;
using EasyTicket.Api.Data.Dto;
using EasyTicket.Api.Data.Models;

namespace EasyTicket.Api.Infrastructure {
    public static class Mapper {
        static Mapper() {
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<PlaceRequestDto, PlaceRequest>();
                cfg.CreateMap<PlaceRequest, PlaceRequestDto>();
            });
        }

        public static TDestination Map<TSource, TDestination>(TSource source) {
            return AutoMapper.Mapper.Map<TSource, TDestination>(source);
        }
    }
}