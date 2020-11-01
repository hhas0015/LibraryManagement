namespace LibraryManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction
    {
        public int TransactionId { get; set; }

        public int? BookId { get; set; }

        public int? StudentId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? IssuedOn { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReturnDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public virtual Book Book { get; set; }

        public virtual Book Book1 { get; set; }
    }
}
