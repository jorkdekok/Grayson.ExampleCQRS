using AutoMapper;
using Domain = Grayson.ExampleCQRS.KmStanden.Domain.AggregatesModel;
using Integration = Grayson.ExampleCQRS.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayson.ExampleCQRS.KmStanden.Infrastructure.Integration
{
    public static class EventsMapping
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Domain.AggregatesModel.KmStandAggregate.KmStandCreated, ExampleCQRS.Integration.Events.KmStandCreated>();
                cfg.CreateMap<Domain.AggregatesModel.KmStandAggregate.KmStandUpdated, ExampleCQRS.Integration.Events.KmStandUpdated>();
            });

        }
    }
}
