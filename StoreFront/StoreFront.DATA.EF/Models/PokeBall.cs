using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class PokeBall
    {
        public PokeBall()
        {
            Pokemons = new HashSet<Pokemon>();
        }

        public int PokeballId { get; set; }
        public string PokeballName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<Pokemon> Pokemons { get; set; }
    }
}
