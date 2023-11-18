using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TARpe22ShopEvert.Core.Dto.WeatherDtos
{
    public class HeadlineDto
    {
        [JsonPropertyName("EffectiveDate")]
        public DateTime EffectiveDate { get; set; }
        [JsonPropertyName("EffectiveEpochDate")]
        public int EffectiveEpochDate { get; set; }
        [JsonPropertyName("Severity")]
        public int Severity { get; set; }
        [JsonPropertyName("Text")]
        public string Text { get; set; }
        [JsonPropertyName("Category")]
        public string Category { get; set; }
        [JsonPropertyName("EndDate")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("EndEpochDate")]
        public int EndEpochDate { get; set; }
        [JsonPropertyName("MobileLink")]
        public string MobileLink { get; set; }
        [JsonPropertyName("Link")]
        public string Link { get; set; }
    }
}
