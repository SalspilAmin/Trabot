
using Tradify.Core.Features.ShipmentTrackings.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.ShipmentTrackingMapping
{
    public partial class ShipmentProfile 
    {
        public void GetShipmentByOrderIdMapping()
        {
            CreateMap<Shipments, GetShipmentByOrderIdResponse>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))


            .ForMember(dest => dest.TrackingNumber, opt => opt.MapFrom(src => src.TrackingNumber))

               .ForMember(dest => dest.CurrentStatus, opt => opt.MapFrom(src => src.CurrentStatus))


               

                         .ForMember(dest => dest.ShipmentTrackings,
        opt => opt.MapFrom(src => src.ShipmentTrackings));


            CreateMap<ShipmentTracking, ShipmentTrackingRespons>()
    .ForMember(dest => dest.Id,
        opt => opt.MapFrom(src => src.Id))

    .ForMember(dest => dest.ShipmentStatus,
        opt => opt.MapFrom(src => src.ShipmentStatus))

    .ForMember(dest => dest.CreatedAt,
        opt => opt.MapFrom(src => src.CreatedAt))

    .ForMember(dest => dest.Notes,
        opt => opt.MapFrom(src => src.Notes));


        }
    }
    }

