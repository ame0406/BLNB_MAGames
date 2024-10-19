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
        public Base_Obj? Game { get; set; }

        public int ComponentId { get; set; }
        [ForeignKey("ComponentId")]
        public Condition? Complicity { get; set; }

        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status? Status { get; set; }
		public Lots? Lot { get; set; }


		//Many to many
		public List<VenteMKP>? VentesMKP { get; set; }
        public List<VenteEbay>? VentesEbay { get; set; }

    }
}
