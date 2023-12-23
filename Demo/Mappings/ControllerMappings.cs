using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models;
using Demo.Service.Models;

namespace Demo.Mappings
{
    public class ControllerMappings : Profile
    {
        public ControllerMappings()
        {
            this.CreateMap<Book, BookInfo>();
            this.CreateMap<BookInfo, Book>();
            this.CreateMap<BookSearchInfo, Book>();
            this.CreateMap<BookResultModel, Book>();
        }
    }
}
