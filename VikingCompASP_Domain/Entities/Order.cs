namespace VikingCompASP_Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; }

        public bool Paid { get; set; }

        public bool Completed { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual Product Product { get; set; }
    }
}
