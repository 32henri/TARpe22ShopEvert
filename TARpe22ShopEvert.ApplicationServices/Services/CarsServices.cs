using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARpe22ShopEvert.Core.Domain;
using TARpe22ShopEvert.Core.Dto;
using TARpe22ShopEvert.Core.ServiceInterface;
using TARpe22ShopEvert.Data;

namespace TARpe22ShopEvert.ApplicationServices.Services
{
    public class CarsServices : ICarsServices
    {
        private readonly TARpe22ShopEvertContext _context;
        private readonly IFilesServices _filesServices;
        public CarsServices
            (
            TARpe22ShopEvertContext context,
            IFilesServices filesServices
            )
        {
            _context = context;
            _filesServices = filesServices;
        }
        public async Task<Car> Create(CarDto dto)
        {
            Car realEstate = new();

            realEstate.Id = Guid.NewGuid();
            realEstate.Mark = dto.Mark;
            realEstate.Model = dto.Model;
            realEstate.Price = dto.Price;
            realEstate.IsNew = dto.IsNew;
            realEstate.HorsePower = dto.HorsePower;
            realEstate.CreatedAt = DateTime.Now;
            _filesServices.FilesToApi(dto, realEstate);


            await _context.RealEstates.AddAsync(realEstate);
            await _context.SaveChangesAsync();
            return realEstate;
        }
        public async Task<Car> Delete(Guid id)
        {
            var realEstateId = await _context.RealEstates
                .Include(x => x.FilesToApi)
                .FirstOrDefaultAsync(x => x.Id == id);
            var images = await _context.FilesToApi
                .Where(x => x.CarId == id)
                .Select(y => new FileToApiDto
                {
                    Id = y.Id,
                    RealEstateId = y.CarId,
                    ExistingFilePath = y.ExistingFilePath
                }).ToArrayAsync();
            await _filesServices.RemoveImagesFromApi(images);
            _context.RealEstates.Remove(realEstateId);
            await _context.SaveChangesAsync();
            return realEstateId;
        }
        public async Task<Car> Update(CarDto dto)
        {
            Car Car = new Car();

            realEstate.Id = dto.Id;
            realEstate.Address = dto.Address;
            realEstate.City = dto.City;
            realEstate.Country = dto.Country;
            realEstate.County = dto.County;
            realEstate.PostalCode = dto.PostalCode;
            realEstate.PhoneNumber = dto.PhoneNumber;
            realEstate.FaxNumber = dto.FaxNumber;
            realEstate.ListingDescription = dto.ListingDescription;
            realEstate.SquareMeters = dto.SquareMeters;
            realEstate.BuildDate = dto.BuildDate;
            realEstate.Price = dto.Price;
            realEstate.RoomCount = dto.RoomCount;
            realEstate.EstateFloor = dto.EstateFloor;
            realEstate.Bathrooms = dto.Bathrooms;
            realEstate.Bedrooms = dto.Bedrooms;
            realEstate.DoesHaveParkingSpace = dto.DoesHaveParkingSpace;
            realEstate.DoesHavePowerGridConnection = dto.DoesHavePowerGridConnection;
            realEstate.DoesHaveWaterGridConnection = dto.DoesHaveWaterGridConnection;
            realEstate.Type = dto.Type;
            realEstate.IsPropertyNewDevelopment = dto.IsPropertyNewDevelopment;
            realEstate.IsPropertySold = dto.IsPropertySold;
            realEstate.CreatedAt = dto.CreatedAt;
            realEstate.ModifiedAt = DateTime.Now;
            _filesServices.FilesToApi(dto, realEstate);

            _context.RealEstates.Update(realEstate);
            await _context.SaveChangesAsync();
            return realEstate;
        }
        public async Task<Car> GetAsync(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
