namespace TARpe22ShopEvert.Models.Spaceship
{
    public class SpaceshipCreateUpdateViewModel
    {
        public Guid Id { get; set; } //unique id
        public int Price { get; set; } //price of the spaceship
        public string Type { get; set; } //spaceship type [Rocket, Saucer, Cruise ship, Cargoship]
        public string Name { get; set; } //name of the ship not build make or model
        public string Description { get; set; } //description of the ship, containing any info not covered by any of the other fields
        public string FuelType { get; set; } //what type of fuel does the spaceship use
        public int FuelCapacity { get; set; } //how much fuel it can hold
        public int FuelConsumption { get; set; } //how much fuel the ship consumes per day on average
        public int PassengerCount { get; set; } //how many passengers fit the ship
        public int EnginePower { get; set; } //how powerful the engine is in kWh
        public bool DoesHaveAutopilot { get; set; } //does the ship have automatic piloting feature
        public int CrewCount { get; set; } //how many people does it take to man the ship
        public int CargoWeight { get; set; } //how much cargo can the ship transport
        public bool DoesHaveLifeSupportSystems { get; set; } //does the ship have life support systems
        public DateTime BuiltDate { get; set; } // when as the ship built at
        public DateTime LastMaintenance { get; set; } //when was the ship last maintained at
        public int MaintenanceCount { get; set; } //how many maintenance sessions has been performed on the ship
        public int FullTripsCount { get; set; } //how many voyages the ship has gone through
        public DateTime MaidenLaunch { get; set; } //when did the ship take its first voyage
        public string Manufacturer { get; set; } //who manufactured the spaceship
        public List<IFormFile> Files { get; set; } //List of files to be added
        public List<ImageViewModel> Image { get; set; } = new List<ImageViewModel>(); //images


        //database info only, do not display to user
        public DateTime CreatedAt { get; set; } //when was the entry created into the database
        public DateTime ModifiedAt { get; set; } //when the entry was last modified at
    }
}
