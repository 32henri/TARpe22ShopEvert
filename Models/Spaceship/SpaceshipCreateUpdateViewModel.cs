using System.ComponentModel.DataAnnotations;
using TARpe22ShopEvert.Core.Domain;
using TARpe22ShopEvert.Core.Dto;

namespace TARpe22ShopEvert.Models.Spaceship
{
    public class SpaceshipCreateUpdateViewModel
    {
        public Guid? Id { get; set; } // globally unique identifier
        public string Name { get; set; } // ship name
        public int Price { get; set; }
        public string FuelType { get; set; }
        public int FuelCapacity { get; set; }
        public string Description { get; set; } // ship description
        //public ICollection<Dimension> Dimensions { get; set; } // contains an pbject of Dimension type, wwhich contains three int values representing width(x) height(y) and depth (z) values
        public int PassengerCount { get; set; } // how many passengers does the ship carry
        public int CrewCount { get; set; } // how many crew members is needed to operate the ship
        public int CargoWeight { get; set; } // how much cargo the ship is able to carry
        public int MaxSpeedInVaccuum { get; set; } // maximum speed after exiting atmosphere
        public DateTime BuiltAtDate { get; set; } // the date this ship was built at
        public DateTime MaidenLaunch { get; set; } // the date that this ship did its first voyage
        public string Manufacturer { get; set; } // company who manufactured the spaceship
        public bool IsSpaceshipPreviouslyOwned { get; set; } // denotes if the ship has been previously owned or not, tldr; second hand identifier.
        public int FullTripsCount { get; set; } // How many round trips has the ship taken
        public string Type { get; set; } // bodytype, build type
        public int EnginePower { get; set; } // engine power in kilowatt
        public int FuelConsumptionPerDay { get; set; } // fuel consumed in a days worth of space traveled at maximum speed
        public int MaintenanceCount { get; set; } // how many maintenance sessions have been conducted on this ship
        public DateTime LastMaintenance { get; set; } // when was the last maintenance performed
        public List<IFormFile> Files { get; set; } // Files that are to be added to this spaceship
        public List<ImageViewModel> Image { get; set; } = new List<ImageViewModel>(); // images themselves that are added


        // only in database

        public DateTime CreatedAt { get; set; } // when the entry was created
        public DateTime ModifiedAt { get; set; } // when the entry has been modified last



    }

}
