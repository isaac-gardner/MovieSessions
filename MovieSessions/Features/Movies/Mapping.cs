using AutoMapper;
using MovieSessions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSessions.Features.Movies
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Add.Command, Movie>();
        }
    }
}
