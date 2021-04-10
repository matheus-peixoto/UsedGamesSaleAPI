using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsedGamesAPI.Models
{
    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Game> Games { get; set; }
    }
}
