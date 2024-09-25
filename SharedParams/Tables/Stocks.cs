using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedParams.Tables
{
    public class Stocks
    {
        [Key]
        public int Id { get; set; }
        public int BoxRate { get; set; }
        public int ManualRate { get; set; }
        public int CDRate { get; set; }
        public string? comments { get; set; } = string.Empty;
        public decimal BuyPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? KeepValue { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public bool ToMaya { get; set; } = false;

        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public Games? Game { get; set; }

        public int SaleTypeId { get; set; }
        [ForeignKey("SaleTypeId")]
        public SaleType? SaleType { get; set; }

        public int ConsoleId { get; set; }
        [ForeignKey("ConsoleId")]
        public ConsoleSystem? ConsoleSystem { get; set; }

        public int ComponentId { get; set; }
        [ForeignKey("ComponentId")]
        public Condition? Complicity { get; set; }

        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        //Many to many
        public List<VenteMKP>? VentesMKP { get; set; }
        public List<VenteEbay>? VentesEbay { get; set; }
        public List<Lots>? Lots { get; set; }

    }
}
