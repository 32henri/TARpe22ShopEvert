using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;
using TARpe22ShopEvert.ApplicationServices.Services;
using TARpe22ShopEvert.Core.Dto;
using TARpe22ShopEvert.Core.ServiceInterface;
using TARpe22ShopEvert.Data;
using TARpe22ShopEvert.Models.Cars;
using TARpe22ShopEvert.Models.RealEstate;
using TARpe22ShopEvert.Models.Spaceship;
using static System.Net.Mime.MediaTypeNames;

namespace TARpe22ShopEvert.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsServices _Car;
        private readonly TARpe22ShopEvertContext _context;
        private readonly IFilesServices _filesServices;
        public CarsController
            (
            ICarsServices Car,
            TARpe22ShopEvertContext context,
            IFilesServices filesServices
            )
        {
            _Car = Car;
            _context = context;
            _filesServices = filesServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.Cars
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new CarIndexViewModel
                {
                    Id = x.Id,
                    Mark = x.Mark,
                    IsNew= x.IsNew,
                    HorsePower = x.HorsePower,
                    Model = x.Model,
                    Price = x.Price,
                    CreatedAt = x.CreatedAt,
                });
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CarCreateUpdateViewModel vm = new();
            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = Guid.NewGuid(),
                Mark = vm.Mark,
                Model = vm.Model,
                IsNew = vm.IsNew,
                Price = vm.Price,
                HorsePower = vm.HorsePower,
                CreatedAt = DateTime.Now,
            };
            var result = await _Car.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var Car = await _Car.GetAsync(id);
            if (Car == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.CarId == id)
                .Select(y => new FileToApiViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();
            var vm = new CarCreateUpdateViewModel();

            vm.Id = Car.Id;
            vm.Mark = Car.Mark;
            vm.Model = Car.Model;
            vm.IsNew = Car.IsNew;
            vm.Price = Car.Price;
            vm.HorsePower = Car.HorsePower;
            vm.CreatedAt = DateTime.Now;
            vm.FileToApiViewModels.AddRange(images);

            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = (Guid)vm.Id,
                Mark = vm.Mark,
                Model = vm.Model,
                IsNew = vm.IsNew,
                County = vm.County,
                SquareMeters = vm.SquareMeters,
                Price = vm.Price,
                PostalCode = vm.PostalCode,
                PhoneNumber = vm.PhoneNumber,
                FaxNumber = vm.FaxNumber,
                ListingDescription = vm.ListingDescription,
                BuildDate = vm.BuildDate,
                RoomCount = vm.RoomCount,
                FloorCount = vm.FloorCount,
                EstateFloor = vm.EstateFloor,
                Bathrooms = vm.Bathrooms,
                Bedrooms = vm.Bedrooms,
                DoesHaveParkingSpace = vm.DoesHaveParkingSpace,
                DoesHavePowerGridConnection = vm.DoesHavePowerGridConnection,
                DoesHaveWaterGridConnection = vm.DoesHaveWaterGridConnection,
                Type = vm.Type,
                IsPropertyNewDevelopment = vm.IsPropertyNewDevelopment,
                IsPropertySold = vm.IsPropertySold,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = DateTime.Now,
                Files = vm.Files,
                FilesToApiDtos = vm.FileToApiViewModels
                .Select(z => new FileToApiDto
                {
                    Id = z.ImageId,
                    ExistingFilePath = z.FilePath,
                    RealEstateId = z.RealEstateId,
                }).ToArray()
            };
            var result = await _realEstates.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var Car = await _realEstates.GetAsync(id);
            if (Car == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.CarId == id)
                .Select(y => new FileToApiViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new RealEstateDetailsViewModel();

            vm.Id = Car.Id;
            vm.Address = Car.Address;
            vm.City = Car.City;
            vm.Country = Car.Country;
            vm.County = Car.County;
            vm.SquareMeters = Car.SquareMeters;
            vm.Price = Car.Price;
            vm.PostalCode = Car.PostalCode;
            vm.PhoneNumber = Car.PhoneNumber;
            vm.FaxNumber = Car.FaxNumber;
            vm.ListingDescription = Car.ListingDescription;
            vm.BuildDate = Car.BuildDate;
            vm.RoomCount = Car.RoomCount;
            vm.FloorCount = Car.FloorCount;
            vm.EstateFloor = Car.EstateFloor;
            vm.Bathrooms = Car.Bathrooms;
            vm.Bedrooms = Car.Bedrooms;
            vm.DoesHaveParkingSpace = Car.DoesHaveParkingSpace;
            vm.DoesHavePowerGridConnection = Car.DoesHavePowerGridConnection;
            vm.DoesHaveWaterGridConnection = Car.DoesHaveWaterGridConnection;
            vm.Type = Car.Type;
            vm.IsPropertyNewDevelopment = Car.IsPropertyNewDevelopment;
            vm.IsPropertySold = Car.IsPropertySold;
            vm.CreatedAt = Car.CreatedAt;
            vm.ModifiedAt = Car.ModifiedAt;
            vm.FileToApiViewModels.AddRange(images);

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var Car = await _realEstates.GetAsync(id);
            if (Car == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.CarId == id)
                .Select(y => new FileToApiViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new RealEstateDeleteViewModel();

            vm.Id = Car.Id;
            vm.Address = Car.Address;
            vm.City = Car.City;
            vm.Country = Car.Country;
            vm.County = Car.County;
            vm.SquareMeters = Car.SquareMeters;
            vm.Price = Car.Price;
            vm.PostalCode = Car.PostalCode;
            vm.PhoneNumber = Car.PhoneNumber;
            vm.FaxNumber = Car.FaxNumber;
            vm.ListingDescription = Car.ListingDescription;
            vm.BuildDate = Car.BuildDate;
            vm.RoomCount = Car.RoomCount;
            vm.FloorCount = Car.FloorCount;
            vm.EstateFloor = Car.EstateFloor;
            vm.Bathrooms = Car.Bathrooms;
            vm.Bedrooms = Car.Bedrooms;
            vm.DoesHaveParkingSpace = Car.DoesHaveParkingSpace;
            vm.DoesHavePowerGridConnection = Car.DoesHavePowerGridConnection;
            vm.DoesHaveWaterGridConnection = Car.DoesHaveWaterGridConnection;
            vm.Type = Car.Type;
            vm.IsPropertyNewDevelopment = Car.IsPropertyNewDevelopment;
            vm.IsPropertySold = Car.IsPropertySold;
            vm.FileToApiViewModels.AddRange(images);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var Car = await _realEstates.Delete(id);
            if (Car == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public async Task<IActionResult> RemoveImage(FileToApiViewModel vm)
        {
            var dto = new FileToApiDto()
            {
                Id = vm.ImageId
            };
            var image = await _filesServices.RemoveImageFromApi(dto);
            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
