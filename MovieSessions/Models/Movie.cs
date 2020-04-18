using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieSessions.Data;

namespace MovieSessions.Models
{
    public class Movie : Entity
    {

        public string Title { get; set; } = "";
        public int Runtime { get; set; } = 0;

        public string? Synopsis { get; set; }

        public string? Director { get; set; }
        public string? Actors { get; set; }
        public string? Poster { get; set; }
        public string? YouTubeEmbed { get; set; }
        public Rating Rating { get; set; } = Rating.Unknown;

    }

}
