using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSessions.Models
{
    public class Session
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public DateTime SessionTime { get; set; }

        public string SpecialSessionText { get; set; }
        
    }
}
