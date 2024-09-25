using System.ComponentModel.DataAnnotations;

namespace SharedParams.Tables
{
    public class Lots
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        public List<Stocks>? Stocks { get; set; }

    }
}
