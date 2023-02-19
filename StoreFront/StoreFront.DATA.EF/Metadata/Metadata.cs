using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.DATA.EF.Models//Metadata
{
    internal class Metadata
    {
        public class CityMetadata
        {
            #region Annotations
            //[Required]
            //[StringLength(100)]
            //[Display(Name = "City")]
            //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
            //[DataType(DataType.PostalCode)]
            //[Range(0, short.MaxValue)]
            #endregion

            //Primary key
            //public int CityId { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
            [Display(Name = "City")]
            public string CityName { get; set; } = null!;
        }

        public class OrderMetadata
        {
            //Primary Key
            //public int OrderId { get; set; }

            public string TrainerId { get; set; } = null!;

            [Required]
            [Display(Name = "Date")]
            [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
            public DateTime OrderDate { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "*Cannot exceed 100 characters")]
            [Display(Name = "Name")]
            public string ShipToName { get; set; } = null!;

            [Required]
            [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
            [Display(Name = "City")]
            public string ShipCity { get; set; } = null!;

            [StringLength(2, ErrorMessage = "*Must ONLY be 2 characters")]
            [Display(Name = "State")]
            public string? ShipState { get; set; }

            [Required]
            [StringLength(5, ErrorMessage = "*Must ONLY be 5 characters")]
            [Display(Name = "Zip")]
            public string ShipZip { get; set; } = null!;
        }

        public class OrderPokemonMetadata
        {
            //Primary Key
            //public int OrderPokemonId { get; set; }

            
            public int OrderId { get; set; }

            [Range(0, short.MaxValue)]
            public short? Quantity { get; set; }

            public int PokemonId { get; set; }

            [Range(0, (double)decimal.MaxValue)]
            [Display(Name = "Price")]
            public decimal? ProductPrice { get; set; }
        }

        public class PokemonMetadata
        {
            //Primary Key
            //public int PokemonId { get; set; }

            [Required]
            [Display(Name = "Name")]
            [StringLength(200, ErrorMessage = "*Cannot exceed 200 characters")]
            public string PokemonName { get; set; } = null!;

            [Display(Name = "Price")]
            [Range(0, (double)Decimal.MaxValue)]
            public decimal? PokemonPrice { get; set; }

            [StringLength(500, ErrorMessage = "*Cannot exceed 500 characters")]
            [Display(Name = "Description")]
            public string? PokemonDescription { get; set; }

            [Required]
            [Display(Name = "Stock")]
            [Range(0, short.MaxValue)]
            public short InStock { get; set; }

            public bool IsDiscontinued { get; set; }
            
            public int? CityId { get; set; }

            [StringLength(75, ErrorMessage = "*Cannot exceed 75 characters")]
            [Display(Name = "Image")]
            public string? PokemonImage { get; set; }

            [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
            [Display(Name = "Ball")]
            public string? PokemonBall { get; set; }
        }

        public class PokemonTypeMetadata
        {
            //Primary Key
            //public int PokemonTypeId { get; set; }
            public int TypeId { get; set; }
            public int PokemonId { get; set; }
        }

        public class TrainerDetailMetadata
        {
            //Primary Key
            //public string TrainerId { get; set; } = null!;

            [Required]
            [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; } = null!;

            [Required]
            [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; } = null!;

            [StringLength(150, ErrorMessage = "*Cannot exceed 150 characers")]
            public string? Address { get; set; }

            [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
            public string? City { get; set; }

            [StringLength(2, ErrorMessage = "*Must ONLY be 2 characters")]
            public string? State { get; set; }

            [StringLength(5, ErrorMessage = "*Must ONLY be 5 characters")]
            public string? Zip { get; set; }

            [StringLength(24, ErrorMessage = "*Cannot exceed 24 characters")]
            [DataType(DataType.PhoneNumber)]
            public string? Phone { get; set; }
        }

        public class TypeMetadata
        {
            //Primary Key
            //public int TypeId { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "*Cannot exceed 50 characters")]
            public string TypeName { get; set; } = null!;
        }
    }
}
