using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StoreFront.DATA.EF.Models//Metadata
{
    internal class Partials
    {
        //Example:
        //[ModelMetadataType(typeof(TypeMetadata))]
        //public partial class TypeMetadata { }

        [ModelMetadataType(typeof(CityMetadata))]
        public partial class CityMetadata { }


        [ModelMetadataType(typeof(OrderMetadata))]
        public partial class OrderMetadata { }


        [ModelMetadataType(typeof(OrderPokemonMetadata))]
        public partial class OrderPokemonMetadata { }


        [ModelMetadataType(typeof(PokemonMetadata))]
        public partial class PokemonMetadata { }


        [ModelMetadataType(typeof(PokemonTypeMetadata))]
        public partial class PokemonTypeMetadata { }


        [ModelMetadataType(typeof(TrainerDetailMetadata))]
        public partial class TrainerDetailMetadata { }


        [ModelMetadataType(typeof(TypeMetadata))]
        public partial class TypeMetadata { }

    }
}
