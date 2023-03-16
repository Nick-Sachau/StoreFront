using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Pokemon
    {
        public Pokemon()
        {
            OrderPokemons = new HashSet<OrderPokemon>();
            PokemonTypes = new HashSet<PokemonType>();
        }

        public int PokemonId { get; set; }
        public string PokemonName { get; set; } = null!;
        public decimal? PokemonPrice { get; set; }
        public string? PokemonDescription { get; set; }
        public short InStock { get; set; }
        public bool IsDiscontinued { get; set; }
        public int? CityId { get; set; }
        public string? PokemonImage { get; set; }
        public int? PokeBallId { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual City? City { get; set; }
        public virtual PokeBall? PokeBall { get; set; }
        public virtual ICollection<OrderPokemon> OrderPokemons { get; set; }
        public virtual ICollection<PokemonType> PokemonTypes { get; set; }
    }
}
