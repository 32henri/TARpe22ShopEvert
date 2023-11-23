using Microsoft.AspNetCore.Mvc;

namespace TARpe22ShopEvert.Models.Cars
{
    public class FileToApiViewModelCar : Controller
    {
        public Guid ImageId { get; set; }
        public string FilePath { get; set; }
        public Guid CarId { get; set; }
    }
}
