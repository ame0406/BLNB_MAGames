using SharedParams.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedParams.DTOs
{
    public class LotsAndContent
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal PrixDachat { get; set; }
        public bool IsActive { get; set; }

        public List<StockContent> Stocks { get; set; } = new();
    }

    public class StockContent
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool ToMaya { get; set; }
        public bool ToBoth { get; set; }
        public decimal? EstimatedSalePrice { get; set; }
        public decimal? SoldPrice { get; set; }
        public DateTime? SoldDate { get; set; }
        public int StatusId { get; set; }
    }

}
