﻿using System;
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
        [Display(Name = "Requestor")]
        public string requestor { get; set; }
        [Display(Name = "Dep't")]
        public string dept { get; set; }
        [Display(Name = "Requestor Approval")]
        public bool deptMNG { get; set; }
        [Display(Name = "LCA Leader")]
        public bool lcaLeader { get; set; }
        [Display(Name = "LCA Mgr")]
        public bool lcaMNG { get; set; }
        [Display(Name = "Requestor")]
        public bool requestorApproval { get; set; }
        [Display(Name = "Requestor Verification")]
        public bool deptMNGConfirm { get; set; }
        [Display(Name = "Comtor")]
        public int comtor { get; set; }
        [Display(Name = "FM")]
        public int fm { get; set; }
        [Display(Name = "GD")]
        public int gd { get; set; }
    }
}