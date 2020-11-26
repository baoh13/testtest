using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Column(TypeName = "VARCHAR(30)")]
        public string CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DeliveryExpected { get; set; }

        public bool ContainsGift { get; set; }

        [Column(TypeName = "VARCHAR(30)")]
        public string ShippingMode { get; set; }

        [Column(TypeName = "VARCHAR(30)")]
        public string OrderSource { get; set; }
    }
}
