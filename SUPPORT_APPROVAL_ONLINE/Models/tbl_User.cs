﻿namespace SUPPORT_APPROVAL_ONLINE.Models
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
        [Column("U_Id")]
        public int id { get; set; }

        public int? group_Id { get; set; }

        public int? permission_Id { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống")]
        [Display(Name = "Tên đăng nhập")]
        [Column("U_username")]
        public string username { get; set; }

        [StringLength(100)]
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [Column("U_password")]
        public string password { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Họ tên")]
        [Column("U_fullname")]
        public string fullname { get; set; }

        [StringLength(20)]
        [Display(Name = "Điện thoại")]
        [Column("U_phone")]
        public string phone { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        [Column("U_email")]
        public string email { get; set; }

        [StringLength(100)]
        [Display(Name = "Người tạo")]
        [Column("U_create_at")]
        public string createAt { get; set; }
        [StringLength(20)]
        [Column("U_stamp")]
        [Display(Name = "Chữ kí")]
        public string stamp { get; set; }
        public virtual tbl_Group tbl_Group { get; set; }

        public virtual tbl_Permission tbl_Permission { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Request> tbl_Request { get; set; }
    }
}
