using System;
using System.Collections.Generic;

namespace StoreFront.DATA.EF.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderPokemons = new HashSet<OrderPokemon>();
        }

        public int OrderId { get; set; }
        public string TrainerId { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public string ShipToName { get; set; } = null!;
        public string ShipCity { get; set; } = null!;
        public string? ShipState { get; set; }
        public string ShipZip { get; set; } = null!;

        public virtual TrainerDetail Trainer { get; set; } = null!;
        public virtual ICollection<OrderPokemon> OrderPokemons { get; set; }
    }
}
