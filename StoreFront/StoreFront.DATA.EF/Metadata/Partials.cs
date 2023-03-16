using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StoreFront.DATA.EF.Models//Metadata
{
    //Example:
    //[ModelMetadataType(typeof(TypeMetadata))]
    //public partial class TypeMetadata { }

    [ModelMetadataType(typeof(CityMetadata))]
    public partial class City { }


    [ModelMetadataType(typeof(OrderMetadata))]
    public partial class Order { }


    [ModelMetadataType(typeof(OrderPokemonMetadata))]
    public partial class OrderPokemon { }


    [ModelMetadataType(typeof(PokemonMetadata))]
    public partial class Pokemon 
    {
        [NotMapped]
        public IFormFile? Image { get; set; }
    }


    [ModelMetadataType(typeof(PokemonTypeMetadata))]
    public partial class PokemonType { }


    [ModelMetadataType(typeof(TrainerDetailMetadata))]
    public partial class TrainerDetail { }


    [ModelMetadataType(typeof(TypeMetadata))]
    public partial class Type { }

    [ModelMetadataType(typeof(PokeBallsMetadata))]
    public partial class PokeBall
    {
        [NotMapped]
        public IFormFile? BallImage { get; set; }
    }
}
