using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Type
    {
        public Type()
        {
            PokemonTypes = new HashSet<PokemonType>();
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; } = null!;

        public virtual ICollection<PokemonType> PokemonTypes { get; set; }
    }
}
