using AutoMapper;
using CQRSExample.Data.Sql.StarterDb;
using CQRSExample.Model.MaterialNumber;
using CQRSExample.Model.Plant;
using CQRSExample.Model.WorkCenter;
using System;

namespace CQRSExample.WebAPI.App_Start
{
    public class AutoMapperConfig
    {
        private static Lazy<MapperConfiguration> _mapConfig = new Lazy<MapperConfiguration>(() =>
        {
            return RegisterMappings();
        });

        private static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
            });
        }

        public static void RegisterStaticConfig()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Plant, PlantDetails>();
                cfg.CreateMap<WorkCenter, WorkCenterDetails>();
                cfg.CreateMap<MaterialNumber, MaterialNumberDetails>();
            });
            Mapper.AssertConfigurationIsValid();
        }

        /// <summary>
        /// TODO add doc
        /// </summary>
        /// <returns></returns>
        public static MapperConfiguration GetMapperConfiguration()
        {
            return _mapConfig.Value;
        }
    }
}