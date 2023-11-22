using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TARpe22ShopEvert.ApplicationServices.Services;
using TARpe22ShopEvert.Core.Dto;
using TARpe22ShopEvert.Core.ServiceInterface;
using TARpe22ShopEvert.Data;
using TARpe22ShopEvert.Models;
using TARpe22ShopEvert.Models.Spaceship;

namespace TARpe22ShopEvert.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly TARpe22ShopEvertContext _context;
        private readonly ISpaceshipsServices _spaceshipsServices;
        private readonly IFilesServices _filesServices;
        public SpaceshipsController
            (
            TARpe22ShopEvertContext context,
            ISpaceshipsServices spaceshipsServices,
            IFilesServices filesServices
            )
        {
            _context = context;
            _spaceshipsServices = spaceshipsServices;
            _filesServices = filesServices;
        }
        public IActionResult Index()
        {
            var result = _context.Spaceships
                .OrderBy(x => x.CreatedAt)
                .Select(x => new SpaceshipIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    PassengerCount = x.PassengerCount,
                    EnginePower = x.EnginePower,
                });
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            SpaceshipCreateUpdateViewModel spaceship = new SpaceshipCreateUpdateViewModel();
            return View("CreateUpdate", spaceship);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SpaceshipCreateUpdateViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Price = vm.Price,
                Description = vm.Description,
                PassengerCount = vm.PassengerCount,
                CrewCount = vm.CrewCount,
                CargoWeight = vm.CargoWeight,
                MaxSpeedInVaccuum = vm.MaxSpeedInVaccuum,
                BuiltAtDate = vm.BuiltAtDate,
                MaidenLaunch = vm.MaidenLaunch,
                Manufacturer = vm.Manufacturer,
                IsSpaceshipPreviouslyOwned = vm.IsSpaceshipPreviouslyOwned,
                FullTripsCount = vm.FullTripsCount,
                Type = vm.Type,
                EnginePower = vm.EnginePower,
                FuelConsumptionPerDay = vm.FuelConsumptionPerDay,
                MaintenanceCount = vm.MaintenanceCount,
                LastMaintenance = vm.LastMaintenance,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    SpaceshipId = x.SpaceshipId,
                }).ToArray()
            };
            var result = await _spaceshipsServices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var spaceship = await _spaceshipsServices.GetAsync(id);
            if (spaceship == null)
            {
                return NotFound();
            }
            var photos = await _context.FilesToDatabase
                .Where(x => x.SpaceshipId == id)
                .Select(y => new ImageViewModel
                {
                    SpaceshipId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new SpaceshipCreateUpdateViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Price = spaceship.Price;
            vm.Description = spaceship.Description;
            vm.PassengerCount = spaceship.PassengerCount;
            vm.CrewCount = spaceship.CrewCount;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.MaxSpeedInVaccuum = spaceship.MaxSpeedInVaccuum;
            vm.BuiltAtDate = spaceship.BuiltAtDate;
            vm.MaidenLaunch = spaceship.MaidenLaunch;
            vm.Manufacturer = spaceship.Manufacturer;
            vm.IsSpaceshipPreviouslyOwned = spaceship.IsSpaceshipPreviouslyOwned;
            vm.FullTripsCount = spaceship.FullTripsCount;
            vm.Type = spaceship.Type;
            vm.EnginePower = spaceship.EnginePower;
            vm.FuelConsumptionPerDay = spaceship.FuelConsumptionPerDay;
            vm.MaintenanceCount = spaceship.MaintenanceCount;
            vm.LastMaintenance = spaceship.LastMaintenance;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;
            vm.Image.AddRange(photos);


            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(SpaceshipCreateUpdateViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Price = vm.Price,
                Description = vm.Description,
                PassengerCount = vm.PassengerCount,
                CrewCount = vm.CrewCount,
                CargoWeight = vm.CargoWeight,
                MaxSpeedInVaccuum = vm.MaxSpeedInVaccuum,
                BuiltAtDate = vm.BuiltAtDate,
                MaidenLaunch = vm.MaidenLaunch,
                Manufacturer = vm.Manufacturer,
                IsSpaceshipPreviouslyOwned = vm.IsSpaceshipPreviouslyOwned,
                FullTripsCount = vm.FullTripsCount,
                Type = vm.Type,
                EnginePower = vm.EnginePower,
                FuelConsumptionPerDay = vm.FuelConsumptionPerDay,
                MaintenanceCount = vm.MaintenanceCount,
                LastMaintenance = vm.LastMaintenance,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    SpaceshipId = x.SpaceshipId,
                }).ToArray()
            };
            var result = await _spaceshipsServices.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var spaceship = await _spaceshipsServices.GetAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var photos = await _context.FilesToDatabase
               .Where(x => x.SpaceshipId == id)
               .Select(y => new ImageViewModel
               {
                   SpaceshipId = y.Id,
                   ImageId = y.Id,
                   ImageData = y.ImageData,
                   ImageTitle = y.ImageTitle,
                   Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
               }).ToArrayAsync();

            var vm = new SpaceshipDetailsViewModel();
            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Price = spaceship.Price;
            vm.Description = spaceship.Description;
            vm.PassengerCount = spaceship.PassengerCount;
            vm.CrewCount = spaceship.CrewCount;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.MaxSpeedInVaccuum = spaceship.MaxSpeedInVaccuum;
            vm.BuiltAtDate = spaceship.BuiltAtDate;
            vm.MaidenLaunch = spaceship.MaidenLaunch;
            vm.Manufacturer = spaceship.Manufacturer;
            vm.IsSpaceshipPreviouslyOwned = spaceship.IsSpaceshipPreviouslyOwned;
            vm.FullTripsCount = spaceship.FullTripsCount;
            vm.Type = spaceship.Type;
            vm.EnginePower = spaceship.EnginePower;
            vm.FuelConsumptionPerDay = spaceship.FuelConsumptionPerDay;
            vm.MaintenanceCount = spaceship.MaintenanceCount;
            vm.LastMaintenance = spaceship.LastMaintenance;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;
            vm.Image.AddRange(photos);
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {

            var spaceship = await _spaceshipsServices.GetAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }

            var photos = await _context.FilesToDatabase
               .Where(x => x.SpaceshipId == id)
               .Select(y => new ImageViewModel
               {
                   SpaceshipId = y.Id,
                   ImageId = y.Id,
                   ImageData = y.ImageData,
                   ImageTitle = y.ImageTitle,
                   Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
               }).ToArrayAsync();
            var vm = new SpaceshipDeleteViewModel();
            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Price = spaceship.Price;
            vm.Description = spaceship.Description;
            vm.PassengerCount = spaceship.PassengerCount;
            vm.CrewCount = spaceship.CrewCount;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.MaxSpeedInVaccuum = spaceship.MaxSpeedInVaccuum;
            vm.BuiltAtDate = spaceship.BuiltAtDate;
            vm.MaidenLaunch = spaceship.MaidenLaunch;
            vm.Manufacturer = spaceship.Manufacturer;
            vm.IsSpaceshipPreviouslyOwned = spaceship.IsSpaceshipPreviouslyOwned;
            vm.FullTripsCount = spaceship.FullTripsCount;
            vm.Type = spaceship.Type;
            vm.EnginePower = spaceship.EnginePower;
            vm.FuelConsumptionPerDay = spaceship.FuelConsumptionPerDay;
            vm.MaintenanceCount = spaceship.MaintenanceCount;
            vm.LastMaintenance = spaceship.LastMaintenance;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;
            vm.Image.AddRange(photos);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid Id)
        {
            var spaceshipId = await _spaceshipsServices.Delete(Id);
            if (spaceshipId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(ImageViewModel file)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = file.ImageId
            };
            var image = await _filesServices.RemoveImage(dto);
            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
