using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TARpe22ShopEvert.Core.Domain;
using TARpe22ShopEvert.Core.Dto;
using TARpe22ShopEvert.Data;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace TARpe22ShopEvert.ApplicationServices.Services
{
    public class FilesServices : IFilesServices
    {
        private readonly TARpe22ShopEvertContext _context;
        private readonly IHostingEnvironment _webHost;
        public FilesServices
            (
                TARpe22ShopEvertContext context,
                IHostingEnvironment webHost
            )
        {
            _context = context;
            _webHost = webHost;
        }
        public void UploadFilesToDatabase(SpaceshipDto dto, Spaceship domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var photo in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        FileToDatabase files = new FileToDatabase()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = photo.FileName,
                            SpaceshipId = domain.Id,
                        };

                        photo.CopyTo(target);
                        files.ImageData = target.ToArray();

                        _context.FilesToDatabase.Add(files);
                    }
                }
            }
        }
        public async Task<FileToDatabase> RemoveImage(FileToDatabaseDto dto)
        {
            var image = await _context.FilesToDatabase
                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync();
            _context.FilesToDatabase.Remove(image);
            await _context.SaveChangesAsync();
            return image;
        }
        public async Task<List<FileToDatabase>> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
        {
            foreach (var dto in dtos)
            {
                var image = await _context.FilesToDatabase
                    .Where(x => x.Id == dto.Id)
                    .FirstOrDefaultAsync();
                _context.FilesToDatabase.Remove(image);
                await _context.SaveChangesAsync();
            }
            return null;
        }

        public void FilesToApi(RealEstateDto dto, RealEstate realEstate)
        {
            string uniqueFileName = null;
            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (!Directory.Exists(_webHost.ContentRootPath + "\\multipleFileUpload\\"))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\multipleFileUpload\\");
                }
                foreach (var image in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                        FileToApi path = new FileToApi
                        {
                            Id = Guid.NewGuid(),
                            ExistingFilePath = uniqueFileName,
                            CarId = realEstate.Id,
                        };
                        _context.FilesToApi.AddAsync(path);
                    }
                }
            }
        }
        public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
        {
            foreach (var dto in dtos)
            {
                var imageId = await _context.FilesToApi
                    .FirstOrDefaultAsync(x => x.ExistingFilePath == dto.ExistingFilePath);
                var filePath = _webHost.ContentRootPath + "\\multipleFileUpload\\" + imageId.ExistingFilePath;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                _context.FilesToApi.Remove(imageId);
                await _context.SaveChangesAsync();
            }
            return null;
        }
        public async Task<FileToApi> RemoveImageFromApi(FileToApiDto dto)
        {
            var imageId = await _context.FilesToApi
                .FirstOrDefaultAsync(x => x.Id == dto.Id);
            var filePath = _webHost.ContentRootPath + "\\multipleFileUpload\\" + imageId.ExistingFilePath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            _context.FilesToApi.Remove(imageId);
            await _context.SaveChangesAsync();
            return null;
        }
    }
}