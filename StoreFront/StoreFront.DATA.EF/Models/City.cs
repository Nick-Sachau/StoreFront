using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class City
    {
        public City()
        {
            Pokemons = new HashSet<Pokemon>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; } = null!;

        public virtual ICollection<Pokemon> Pokemons { get; set; }
    }
}
