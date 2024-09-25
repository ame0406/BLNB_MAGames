﻿using System.ComponentModel.DataAnnotations;

namespace SharedParams.Tables
{
    public class SaleType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

    }
}
