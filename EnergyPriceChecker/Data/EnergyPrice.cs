using System.ComponentModel.DataAnnotations;

namespace EnergyPriceChecker.Data;

public class EnergyPrice
{
    [Key]
    public int PriceId { get; }
    public decimal Price { get; set; }
    public string EnergyType { get; set; }
    public string ReadingDate { get; set; }
}
