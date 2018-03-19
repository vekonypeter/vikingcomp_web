namespace VikingCompASP_Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("News")]
    public partial class News
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Head { get; set; }

        public string Body { get; set; }

        public DateTime? Date { get; set; }
    }
}
