using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EnergyPriceChecker.Service.Models;

public class PricesResponseDto
{
    [JsonPropertyName("Prices")]
    public List<Prices> Prices { get; set; }
    public int IntervalType { get; set; }
    public decimal Average { get; set; }
    public string FromDate { get; set; }
    public string TillDate { get; set; }


}
