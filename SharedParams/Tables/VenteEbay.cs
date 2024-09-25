using System.ComponentModel.DataAnnotations;

namespace SharedParams.Tables
{
    public class VenteEbay
    {
        [Key]
        public int Id { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? ShippingPrice { get; set; }
        public string? Link { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }

        public List<Stocks>? Stocks { get; set; }
    }
}
