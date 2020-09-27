namespace LibraryManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Tfn { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Dob { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        public int? DepartmentId { get; set; }

        public int? RoleId { get; set; }

        public virtual Department Department { get; set; }

        public virtual Role Role { get; set; }
    }
}
