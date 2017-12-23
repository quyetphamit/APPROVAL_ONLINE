namespace SUPPORT_APPROVAL_ONLINE.Models.abc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Request
    {
        public int id { get; set; }

        public int U_Id_Create { get; set; }

        public int U_Id_Send { get; set; }

        public int U_Id_Approval { get; set; }

        public int? U_Id_Dept_MNG { get; set; }

        public int? U_Id_LCA_Leader { get; set; }

        public int? U_Id_LCA_MNG { get; set; }

        public int? U_Id_Comtor { get; set; }

        public int? U_Id_FM { get; set; }

        public int? U_Id_GD { get; set; }

        public int customer_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_Dept_MNG_Approval { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_LCA_Leader_Approval { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_LCA_MNG_Approval { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_Comtor_Approval { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_FM_Approval { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_GD_Approval { get; set; }

        public int quantity { get; set; }

        [Column(TypeName = "date")]
        public DateTime dealLine { get; set; }

        [StringLength(200)]
        public string title { get; set; }

        public bool increaseProductivity { get; set; }

        public bool newModel { get; set; }

        public bool increaseProduction { get; set; }

        public bool improve { get; set; }

        [Column("_5s")]
        public bool C_5s { get; set; }

        public bool checkJig { get; set; }

        public int? reducePeple { get; set; }

        [StringLength(200)]
        public string errorContent { get; set; }

        [StringLength(10)]
        public string currentError { get; set; }

        [StringLength(10)]
        public string afterError { get; set; }

        public int? cost_Savings { get; set; }

        [StringLength(100)]
        public string other { get; set; }

        [StringLength(100)]
        public string pay { get; set; }

        [StringLength(100)]
        public string model { get; set; }

        [StringLength(100)]
        public string pcb { get; set; }

        [Column(TypeName = "ntext")]
        public string contentDetail { get; set; }

        public int? cost { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_Create { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_Update { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_Received { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_Finish { get; set; }

        [StringLength(50)]
        public string file_upload { get; set; }

        [StringLength(50)]
        public string file_upload_update { get; set; }

        [StringLength(50)]
        public string costDetail_upload { get; set; }

        [StringLength(200)]
        public string comment { get; set; }

        public virtual tbl_Customer tbl_Customer { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
