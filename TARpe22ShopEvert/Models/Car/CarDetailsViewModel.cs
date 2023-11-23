using Microsoft.AspNetCore.Mvc;
using TARpe22ShopEvert.Models.RealEstate;

namespace TARpe22ShopEvert.Models.Cars
{
    public class CarDetailsViewModel : Controller
    {
        public Guid Id { get; set; }
        public string Mark { get; set; }
        public int Price { get; set; }
        public string Model { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsNew { get; set; }
        public int HorsePower { get; set; }
        public List<FileToApiViewModelCar> FileToApiViewModelCar { get; set; } = new List<FileToApiViewModelCar>();
    }
}
