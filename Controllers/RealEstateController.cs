using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Xml;
using TARpe22ShopEvert.ApplicationServices.Services;
using TARpe22ShopEvert.Core.Domain;
using TARpe22ShopEvert.Core.Dto;
using TARpe22ShopEvert.Core.Dto.ServiceInterface;
using TARpe22ShopEvert.Core.ServiceInterface;
using TARpe22ShopEvert.Data;
using TARpe22ShopEvert.Models.RealEstate;

namespace TARpe22ShopEvert.Controllers
{
    public class RealEstatesController : Controller
    {
        private readonly IRealEstatesServices _realEstatesServices;
        private readonly TARpe22ShopEvertContext _context;
        private readonly IFilesServices _filesServices;
        public RealEstatesController(IRealEstatesServices realEstatesServices, TARpe22ShopEvertContext context, IFilesServices filesServices)
        {
            _realEstatesServices = realEstatesServices;
            _context = context;
            _filesServices = filesServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.RealEstates
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new RealEstateIndexViewModel
                {
                    //{ return (Country + ", " + County + ", " + City + ", " + Address); }
                    Id = x.Id,
                    Location = x.Country + ", " + x.County + ", " + x.City + ", " + x.Address,
                    SquareMeters = x.SquareMeters,
                    RoomCount = x.RoomCount,
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
                Type = vm.Type,
                ListingDescription = vm.ListingDescription,
                Address = vm.Address,
                County = vm.County,
                Country = vm.Country,
                City = vm.City,
                PostalCode = vm.PostalCode,
                ContactPhone = vm.ContactPhone,
                ContactFax = vm.ContactFax,
                SquareMeters = vm.SquareMeters,
                Floor = vm.Floor,
                FloorCount = vm.FloorCount,
                Price = vm.Price,
                RoomCount = vm.RoomCount,
                BedroomCount = vm.BedroomCount,
                BathroomCount = vm.BathroomCount,
                WhenEstateListedAt = vm.WhenEstateListedAt,
                IsPropertySold = vm.IsPropertySold,
                DoesHaveSwimmingPool = vm.DoesHaveSwimmingPool,
                BuiltAt = vm.BuiltAt,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Files = vm.Files,
                FileToApiDtos = vm.FileToApiViewModels
                .Select(x => new FileToApiDto
                {
                    Id = x.ImageId,
                    ExistingFilePath = x.FilePath,
                    RealEstateId = x.RealEstateId,
                }).ToArray()
            };
            var result = await _realEstatesServices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", vm);

        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realEstate = await _realEstatesServices.GetAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.RealEstateId == id)
                .Select(y => new FileToApiViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();
            var vm = new RealEstateCreateUpdateViewModel();
            vm.Id = realEstate.Id;
            vm.Type = realEstate.Type;
            vm.ListingDescription = realEstate.ListingDescription;
            vm.Address = realEstate.Address;
            vm.County = realEstate.County;
            vm.Country = realEstate.Country;
            vm.City = realEstate.City;
            vm.PostalCode = realEstate.PostalCode;
            vm.ContactPhone = realEstate.ContactPhone;
            vm.ContactFax = realEstate.ContactFax;
            vm.SquareMeters = realEstate.SquareMeters;
            vm.Floor = realEstate.Floor;
            vm.FloorCount = realEstate.FloorCount;
            vm.Price = realEstate.Price;
            vm.RoomCount = realEstate.RoomCount;
            vm.BedroomCount = realEstate.BedroomCount;
            vm.BathroomCount = realEstate.BathroomCount;
            vm.WhenEstateListedAt = realEstate.WhenEstateListedAt;
            vm.IsPropertySold = realEstate.IsPropertySold;
            vm.DoesHaveSwimmingPool = realEstate.DoesHaveSwimmingPool;
            vm.BuiltAt = realEstate.BuiltAt;
            vm.CreatedAt = realEstate.CreatedAt;
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
                Type = vm.Type,
                ListingDescription = vm.ListingDescription,
                Address = vm.Address,
                County = vm.County,
                Country = vm.Country,
                City = vm.City,
                PostalCode = vm.PostalCode,
                ContactPhone = vm.ContactPhone,
                ContactFax = vm.ContactFax,
                SquareMeters = vm.SquareMeters,
                Floor = vm.Floor,
                FloorCount = vm.FloorCount,
                Price = vm.Price,
                RoomCount = vm.RoomCount,
                BedroomCount = vm.BedroomCount,
                BathroomCount = vm.BathroomCount,
                WhenEstateListedAt = vm.WhenEstateListedAt,
                IsPropertySold = vm.IsPropertySold,
                DoesHaveSwimmingPool = vm.DoesHaveSwimmingPool,
                BuiltAt = vm.BuiltAt,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Files = vm.Files,
                FileToApiDtos = vm.FileToApiViewModels
                .Select(x => new FileToApiDto
                {
                    Id = x.ImageId,
                    ExistingFilePath = x.FilePath,
                    RealEstateId = x.RealEstateId,
                }).ToArray()
            };
            var result = await _realEstatesServices.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var realEstate = await _realEstatesServices.GetAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
            .Where(x => x.RealEstateId == id)
            .Select(y => new FileToApiViewModel
            {
                FilePath = y.ExistingFilePath,
                ImageId = y.Id
            }).ToArrayAsync();
            var vm = new RealEstateDetailsViewModel(); //todo: details viewmodel
            vm.Id = realEstate.Id;
            vm.Type = realEstate.Type;
            vm.ListingDescription = realEstate.ListingDescription;
            vm.Address = realEstate.Address;
            vm.County = realEstate.County;
            vm.Country = realEstate.Country;
            vm.City = realEstate.City;
            vm.PostalCode = realEstate.PostalCode;
            vm.ContactPhone = realEstate.ContactPhone;
            vm.ContactFax = realEstate.ContactFax;
            vm.SquareMeters = realEstate.SquareMeters;
            vm.Floor = realEstate.Floor;
            vm.FloorCount = realEstate.FloorCount;
            vm.Price = realEstate.Price;
            vm.RoomCount = realEstate.RoomCount;
            vm.BedroomCount = realEstate.BedroomCount;
            vm.BathroomCount = realEstate.BathroomCount;
            vm.WhenEstateListedAt = realEstate.WhenEstateListedAt;
            vm.IsPropertySold = realEstate.IsPropertySold;
            vm.DoesHaveSwimmingPool = realEstate.DoesHaveSwimmingPool;
            vm.BuiltAt = realEstate.BuiltAt;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;
            vm.FileToApiViewModels.AddRange(images);

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realEstate = await _realEstatesServices.GetAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
            .Where(x => x.RealEstateId == id)
            .Select(y => new FileToApiViewModel
            {
                FilePath = y.ExistingFilePath,
                ImageId = y.Id
            }).ToArrayAsync();
            var vm = new RealEstateDeleteViewModel();
            vm.Id = realEstate.Id;
            vm.Type = realEstate.Type;
            vm.ListingDescription = realEstate.ListingDescription;
            vm.Address = realEstate.Address;
            vm.County = realEstate.County;
            vm.Country = realEstate.Country;
            vm.City = realEstate.City;
            vm.PostalCode = realEstate.PostalCode;
            vm.ContactPhone = realEstate.ContactPhone;
            vm.ContactFax = realEstate.ContactFax;
            vm.SquareMeters = realEstate.SquareMeters;
            vm.Floor = realEstate.Floor;
            vm.FloorCount = realEstate.FloorCount;
            vm.Price = realEstate.Price;
            vm.RoomCount = realEstate.RoomCount;
            vm.BedroomCount = realEstate.BedroomCount;
            vm.BathroomCount = realEstate.BathroomCount;
            vm.WhenEstateListedAt = realEstate.WhenEstateListedAt;
            vm.IsPropertySold = realEstate.IsPropertySold;
            vm.DoesHaveSwimmingPool = realEstate.DoesHaveSwimmingPool;
            vm.BuiltAt = realEstate.BuiltAt;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;
            vm.FileToApiViewModels.AddRange(images);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realEstate = await _realEstatesServices.Delete(id);
            if (realEstate != null)
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
