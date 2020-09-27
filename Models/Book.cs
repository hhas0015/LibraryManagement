namespace LibraryManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Book
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        [StringLength(100)]
        public string Publisher { get; set; }

        [Column(TypeName = "date")]
        public DateTime? YearOfPublication { get; set; }

        public int? NoOfPages { get; set; }

        [StringLength(10)]
        public string Available { get; set; }
    }
}
