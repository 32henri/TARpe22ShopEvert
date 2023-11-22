using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;
using TARpe22ShopEvert.ApplicationServices.Services;
using TARpe22ShopEvert.Core.Dto;
using TARpe22ShopEvert.Core.ServiceInterface;
using TARpe22ShopEvert.Data;
using TARpe22ShopEvert.Models.RealEstate;
using TARpe22ShopEvert.Models.Spaceship;
using static System.Net.Mime.MediaTypeNames;

namespace TARpe22ShopEvert.Controllers
{
    public class RealEstatesController : Controller
    {
        private readonly IRealEstatesServices _realEstates;
        private readonly TARpe22ShopEvertContext _context;
        private readonly IFilesServices _filesServices;
        public RealEstatesController
            (
            IRealEstatesServices realEstates,
            TARpe22ShopEvertContext context,
            IFilesServices filesServices
            )
        {
            _realEstates = realEstates;
            _context = context;
            _filesServices = filesServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.RealEstates
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Address = x.Address,
                    City = x.City,
                    Country = x.Country,
                    SquareMeters = x.SquareMeters,
                    Price = x.Price,
                    IsPropertySold = x.IsPropertySold,
                });
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel vm = new();
            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = Guid.NewGuid(),
                Address = vm.Address,
                City = vm.City,
                Country = vm.Country,
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
                CreatedAt = DateTime.Now,
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
            var result = await _realEstates.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realEstate = await _realEstates.GetAsync(id);
            if (realEstate == null)
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
            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = realEstate.Id;
            vm.Address = realEstate.Address;
            vm.City = realEstate.City;
            vm.Country = realEstate.Country;
            vm.County = realEstate.County;
            vm.SquareMeters = realEstate.SquareMeters;
            vm.Price = realEstate.Price;
            vm.PostalCode = realEstate.PostalCode;
            vm.PhoneNumber = realEstate.PhoneNumber;
            vm.FaxNumber = realEstate.FaxNumber;
            vm.ListingDescription = realEstate.ListingDescription;
            vm.BuildDate = realEstate.BuildDate;
            vm.RoomCount = realEstate.RoomCount;
            vm.FloorCount = realEstate.FloorCount;
            vm.EstateFloor = realEstate.EstateFloor;
            vm.Bathrooms = realEstate.Bathrooms;
            vm.Bedrooms = realEstate.Bedrooms;
            vm.DoesHaveParkingSpace = realEstate.DoesHaveParkingSpace;
            vm.DoesHavePowerGridConnection = realEstate.DoesHavePowerGridConnection;
            vm.DoesHaveWaterGridConnection = realEstate.DoesHaveWaterGridConnection;
            vm.Type = realEstate.Type;
            vm.IsPropertyNewDevelopment = realEstate.IsPropertyNewDevelopment;
            vm.IsPropertySold = realEstate.IsPropertySold;
            vm.CreatedAt = DateTime.Now;
            vm.ModifiedAt = DateTime.Now;
            vm.FileToApiViewModels.AddRange(images);

            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = (Guid)vm.Id,
                Address = vm.Address,
                City = vm.City,
                Country = vm.Country,
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
            var realEstate = await _realEstates.GetAsync(id);
            if (realEstate == null)
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

            vm.Id = realEstate.Id;
            vm.Address = realEstate.Address;
            vm.City = realEstate.City;
            vm.Country = realEstate.Country;
            vm.County = realEstate.County;
            vm.SquareMeters = realEstate.SquareMeters;
            vm.Price = realEstate.Price;
            vm.PostalCode = realEstate.PostalCode;
            vm.PhoneNumber = realEstate.PhoneNumber;
            vm.FaxNumber = realEstate.FaxNumber;
            vm.ListingDescription = realEstate.ListingDescription;
            vm.BuildDate = realEstate.BuildDate;
            vm.RoomCount = realEstate.RoomCount;
            vm.FloorCount = realEstate.FloorCount;
            vm.EstateFloor = realEstate.EstateFloor;
            vm.Bathrooms = realEstate.Bathrooms;
            vm.Bedrooms = realEstate.Bedrooms;
            vm.DoesHaveParkingSpace = realEstate.DoesHaveParkingSpace;
            vm.DoesHavePowerGridConnection = realEstate.DoesHavePowerGridConnection;
            vm.DoesHaveWaterGridConnection = realEstate.DoesHaveWaterGridConnection;
            vm.Type = realEstate.Type;
            vm.IsPropertyNewDevelopment = realEstate.IsPropertyNewDevelopment;
            vm.IsPropertySold = realEstate.IsPropertySold;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;
            vm.FileToApiViewModels.AddRange(images);

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realEstate = await _realEstates.GetAsync(id);
            if (realEstate == null)
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

            vm.Id = realEstate.Id;
            vm.Address = realEstate.Address;
            vm.City = realEstate.City;
            vm.Country = realEstate.Country;
            vm.County = realEstate.County;
            vm.SquareMeters = realEstate.SquareMeters;
            vm.Price = realEstate.Price;
            vm.PostalCode = realEstate.PostalCode;
            vm.PhoneNumber = realEstate.PhoneNumber;
            vm.FaxNumber = realEstate.FaxNumber;
            vm.ListingDescription = realEstate.ListingDescription;
            vm.BuildDate = realEstate.BuildDate;
            vm.RoomCount = realEstate.RoomCount;
            vm.FloorCount = realEstate.FloorCount;
            vm.EstateFloor = realEstate.EstateFloor;
            vm.Bathrooms = realEstate.Bathrooms;
            vm.Bedrooms = realEstate.Bedrooms;
            vm.DoesHaveParkingSpace = realEstate.DoesHaveParkingSpace;
            vm.DoesHavePowerGridConnection = realEstate.DoesHavePowerGridConnection;
            vm.DoesHaveWaterGridConnection = realEstate.DoesHaveWaterGridConnection;
            vm.Type = realEstate.Type;
            vm.IsPropertyNewDevelopment = realEstate.IsPropertyNewDevelopment;
            vm.IsPropertySold = realEstate.IsPropertySold;
            vm.FileToApiViewModels.AddRange(images);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realEstate = await _realEstates.Delete(id);
            if (realEstate == null)
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