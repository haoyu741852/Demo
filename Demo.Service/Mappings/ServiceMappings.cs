using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Demo.Repository.Models;
using Demo.Service.Models;

namespace Demo.Service.Mappings
{
    public class ServiceMappings : Profile
    {
        public ServiceMappings()
        {
            this.CreateMap<BookInfo, BookCondition>();
            this.CreateMap<BookSearchInfo, BookSearchCondition>();
            this.CreateMap<BookDataModel, BookResultModel>();
        }
    }
}
