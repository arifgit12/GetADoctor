using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetADoctor.Web.Infrastructure.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config => 
            {

            });
        }
    }
}