using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TARpe22ShopEvert.Core.Domain
{
    public class FileToApi
    {
        public Guid Id { get; set; }
        public string ExistingFilePath { get; set; }
        public Guid? RealEstateId { get; set; }
    }
}
