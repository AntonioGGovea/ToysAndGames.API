using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToysAndGames.Data.Models;
using ToysAndGames.DTOs;

namespace ToysAndGames.Services.Mappers
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
        }
    }
}
