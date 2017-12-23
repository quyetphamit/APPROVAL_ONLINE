namespace SUPPORT_APPROVAL_ONLINE.Models.abc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_User()
        {
            tbl_Request = new HashSet<tbl_Request>();
        }

        [Key]
        public int U_Id { get; set; }

        public int? group_Id { get; set; }

        public int? permission_Id { get; set; }

        [Required]
        [StringLength(20)]
        public string U_username { get; set; }

        [Required]
        [StringLength(100)]
        public string U_password { get; set; }

        [Required]
        [StringLength(50)]
        public string U_fullname { get; set; }

        [StringLength(20)]
        public string U_phone { get; set; }

        [StringLength(50)]
        public string U_email { get; set; }

        [StringLength(100)]
        public string U_create_at { get; set; }

        [StringLength(20)]
        public string U_stamp { get; set; }

        public virtual tbl_Group tbl_Group { get; set; }

        public virtual tbl_Permission tbl_Permission { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Request> tbl_Request { get; set; }
    }
}
