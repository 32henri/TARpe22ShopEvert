using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TARpe22ShopEvert.Core.Domain
{
    public enum CarType
    {
        BMW, Audi, Honda, Toyota, Bently, Subaru, Jeep, Lamborghini, Ford
    }

    public class CarDto
    {
        public Guid Id { get; set; }
        public string Mark { get; set; }
        public int Price { get; set; }
        public string Model { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsNew { get; set; }
        public int HorsePower { get; set; }

    }
}
