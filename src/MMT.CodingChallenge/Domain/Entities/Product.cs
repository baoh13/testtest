using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string ProductName { get; set; }

        [Column(TypeName = "decimal(9, 2)")]
        public decimal? PackHeight { get; set; } // Nullable

        [Column(TypeName = "decimal(9, 2)")]
        public decimal? PackWidth { get; set; } // Nullable

        [Column(TypeName = "decimal(8, 3)")]
        public decimal? PackWeight { get; set; } // Nullable

        [Column(TypeName = "VARCHAR(20)")]
        public string Colour { get; set; }

        [Column(TypeName = "VARCHAR(20)")]
        public string Size { get; set; }
    }
}
