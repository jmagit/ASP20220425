using System;
using System.ComponentModel.DataAnnotations;

namespace DemosRest {
    public class WeatherForecast {
        public DateTime Date { get; set; }

        [Range(-100, 100)]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [MaxLength(500)]
        public string Summary { get; set; }
    }
}
