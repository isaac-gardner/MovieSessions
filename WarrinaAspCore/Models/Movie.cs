using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarrinaAspCore.Data;

namespace WarrinaAspCore.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public bool Enable { get; set; }
        public string ExternalId { get; set; }
        public string Title { get; set; }
        public int MinutesRuntime { get; set; }

        public string Synopsis { get; set; }

        public string Director { get; set; }
        public string Stars { get; set; }
        public string ImgLocation { get; set; }
        public Rating Rating { get; set; }

    }

}
