using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            var config = new MapperConfiguration(cfg =>
            {
            });
            config.AssertConfigurationIsValid();
            Mapper.Initialize(cfg =>
            {
            });
            Mapper.AssertConfigurationIsValid();
            return config;
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