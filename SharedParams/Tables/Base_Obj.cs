using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedParams.Tables
{
	public class Base_Obj
	{
        [Key]
        public int Id { get; set; }
		public string Name { get; set; } = string.Empty;	//All
		public string? Edition { get; set; } = string.Empty;	//All
		public int? GoSpace { get; set; }	//Hardware
		public short TypeObj { get; set; }	//All
		public bool IsActive { get; set; } = true;
		public string? Barcode { get; set; } 
        public List<ObjImages>? lstImages { get; set; } = new List<ObjImages>();

		public int SaleTypeId { get; set; }
		[ForeignKey("SaleTypeId")]
		public SaleType? SaleType { get; set; } = new SaleType();
		public int? MarqueId { get; set; } //Conosle, Games
		[ForeignKey("MarqueId")]
		public Marques? Marque { get; set; } = new Marques();
	}
}
