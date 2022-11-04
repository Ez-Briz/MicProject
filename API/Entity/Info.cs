using System.ComponentModel;
using API.Helpers;
using Newtonsoft.Json;

namespace API.Entity
{
    public partial class Info
    {
        [JsonProperty("ROW")]
        public Row Row { get; set; }
    }

    public partial class Row
    {
        [JsonProperty("VUNP")]
        public long Vunp { get; set; }

        [JsonProperty("VNAIMP")]
        public string Vnaimp { get; set; }

        [JsonProperty("VNAIMK")]
        public string Vnaimk { get; set; }

        [JsonProperty("VPADRES")]
        public string Vpadres { get; set; }

        [JsonProperty("DREG")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Dreg { get; set; }

        [JsonProperty("NMNS")]
        public string Nmns { get; set; }

        [JsonProperty("VMNS")]
        public string Vmns { get; set; }

        [JsonProperty("CKODSOST")]
        public string Ckodsost { get; set; }

        [JsonProperty("VKODS")]
        public string Vkods { get; set; }

        [JsonProperty("DLIKV")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? Dlikv { get; set; }

        [JsonProperty("VLIKV")]
        public string Vlikv { get; set; }
    }
}