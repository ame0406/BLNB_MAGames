using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedParams.Tables
{
    public class Stocks
    {
        [Key]
        public int Id { get; set; }
        public int? BoxRate { get; set; }
        public int? ManualRate { get; set; }
        public int? CDRate { get; set; }
        public string? Comments { get; set; } = string.Empty;
        public decimal? BuyPrice { get; set; }
        public decimal EstimatedSalePrice { get; set;} = 0;
        public decimal? KeepValue { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public DateTime? SoldDate { get; set; }
        public decimal? SoldPrice { get; set; }
        public bool IsActive { get; set; } = true;
        public bool ToMaya { get; set; } = false;
        public bool ToBoth { get; set; } = false;
        public decimal? BuyPriceForWhoToWhoIsTrue { get; set; }


        public int BaseObjId { get; set; }
        [ForeignKey("BaseObjId")]
        public Base_Obj? BaseObj { get; set; } = new Base_Obj();

        public int ConditionId { get; set; }
        [ForeignKey("ConditionId")]
        public Condition? Condition { get; set; } = new Condition();

        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status? Status { get; set; } = new Status();
		public Lots? Lot { get; set; } = null;
		public Lots? LotEchange { get; set; } = null;


        //Many to many
        public List<VenteMKP>? VentesMKP { get; set; } = new List<VenteMKP>();
        public List<VenteEbay>? VentesEbay { get; set; }= new List<VenteEbay>();    

    }
}
