using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.BL.DTOs
{
    public class DonutExampleDTO
    {
        [JsonProperty("value")]
        public float Value { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
