using EasyTicket.Api.Dto;
using EasyTicket.SharedResources.Models;

namespace EasyTicket.Api.Infrastructure {
    public static class Mapper {
        static Mapper() {
            AutoMapper.Mapper.Initialize(cfg => { cfg.CreateMap<RequestDto, Request>().ReverseMap(); });
        }

        public static TDestination Map<TSource, TDestination>(TSource source) {
            return AutoMapper.Mapper.Map<TSource, TDestination>(source);
        }
    }
}