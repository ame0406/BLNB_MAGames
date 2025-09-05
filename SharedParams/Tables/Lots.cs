using System.ComponentModel.DataAnnotations;

namespace SharedParams.Tables
{
    public class Lots
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public decimal PrixDachat { get; set; } 
        public decimal? PrixDachatForWhoToWhoIsTrue { get; set; } 
        public bool IsActive { get; set; } = true;
    }
}
