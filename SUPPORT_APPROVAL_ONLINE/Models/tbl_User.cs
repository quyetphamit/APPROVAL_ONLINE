namespace SUPPORT_APPROVAL_ONLINE.Models
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

        [StringLength(20)]
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
        [Display(Name = "Tên đăng nhập")]
        public string U_username { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        public string U_password { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Họ tên")]
        public string U_fullname { get; set; }

        [StringLength(20)]
        [Display(Name = "Điện thoại")]
        public string U_phone { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        public string U_email { get; set; }

        [StringLength(100)]
        [Display(Name = "Người tạo")]
        public string U_create_at { get; set; }

        public virtual tbl_Group tbl_Group { get; set; }

        public virtual tbl_Permission tbl_Permission { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Request> tbl_Request { get; set; }
    }
}
