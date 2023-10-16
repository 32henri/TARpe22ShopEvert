namespace TARpe22ShopEvert.Models.Spaceship
{
    public class SpaceshipIndexViewModel
    {
        public Guid Id { get; set; }  // unique id
        public int Price { get; set; } // price of the spaceship
        public string Type { get; set; } // spaceship type [Rocket, Saucer, Cruise ship, Cargoship]
        public string Name { get; set; } // name of the spaceship not build make or model
        public string Description { get; set; } // description of ship
        public string FuelType { get; set; } // what type of fuel does ship use
        public int FuelCapacity { get; set; } // how much fuel it can hold
        public int FuelConsumption { get; set; } // how much fuel the ship consumes per day
        public int EnginePower { get; set; } // how powerful engine is
        public int PassengerAmount { get; set; }
        public bool DoesHaveautopilot { get; set; }
        public int CrewCount { get; set; }
        public int CargoWeight { get; set; }
        public bool DoesHaveLifeSupportSystems { get; set; }
        public DateTime BuiltDate { get; set; } // when ship was built
        public DateTime LastMaintenance { get; set; }
        public int MaintenanceCount { get; set; }
        public int FullTripsCount { get; set; } // how many voyages the ship hase gone through
        public DateTime MaidenLaunch { get; set; } // first voyage
        public string Manufacturer { get; set; }

        //database info only, do not display for user

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
