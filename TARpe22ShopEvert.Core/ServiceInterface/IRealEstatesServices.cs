using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARpe22ShopEvert.Core.Domain;
using TARpe22ShopEvert.Core.Dto;

namespace TARpe22ShopEvert.Core.ServiceInterface
{
    public interface IRealEstatesServices
    {
        Task<RealEstate> GetAsync(Guid id);
        Task<RealEstate> Create(RealEstateDto dto);
        Task<RealEstate> Update(RealEstateDto dto);
        Task<RealEstate> Delete(Guid id);
    }
}
