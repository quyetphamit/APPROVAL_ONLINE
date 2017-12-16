using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SUPPORT_APPROVAL_ONLINE.Models
{
    public class RequestStatus
    {
        public int IdOrigin { get; set; }
        [Display(Name = "Id")]
        public string id { get; set; }
        [Display(Name = "Title")]
        public string title { get; set; }
        [Display(Name = "Người yêu cầu")]
        public string requestor { get; set; }
        [Display(Name = "Requestor Approval")]
        public bool deptMNG { get; set; }
        [Display(Name = "LCA Leader")]
        public bool lcaLeader { get; set; }
        [Display(Name = "LCA Mgr")]
        public bool lcaMNG { get; set; }
        [Display(Name = "Requestor Verification")]
        public bool deptMNGConfirm { get; set; }
        [Display(Name = "FM")]
        public bool fm { get; set; }
        [Display(Name = "GD")]
        public bool gd { get; set; }
    }
}