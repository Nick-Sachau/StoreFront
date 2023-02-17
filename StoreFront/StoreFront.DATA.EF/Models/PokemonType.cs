using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class PokemonType
    {
        public int PokemonTypeId { get; set; }
        public int TypeId { get; set; }
        public int PokemonId { get; set; }

        public virtual Pokemon Pokemon { get; set; } = null!;
        public virtual Type Type { get; set; } = null!;
    }
}
