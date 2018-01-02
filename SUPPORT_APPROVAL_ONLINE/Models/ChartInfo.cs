using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SUPPORT_APPROVAL_ONLINE.Models
{
    public class ChartInfo
    {
        [Display(Name ="LCA")]
        public int lcaTotal { get; set; }
        public int lcaSuccess { get; set; }
        [Display(Name = "MC & PC")]
        public int mcpcTotal { get; set; }
        public int mcpcSuccess { get; set; }
        [Display(Name = "PE-Maemura")]
        public int pe1Total { get; set; }
        public int pe1Success { get; set; }
        [Display(Name = "PE-Kaneko")]
        public int pe2Total { get; set; }
        public int pe2Success { get; set; }
        [Display(Name = "PE-Murayama")]
        public int pe3Total { get; set; }
        public int pe3Success { get; set; }
        [Display(Name = "PD1")]
        public int pd1Total { get; set; }
        public int pd1Success { get; set; }
        [Display(Name = "PD2")]
        public int pd2Total { get; set; }
        public int pd2Success { get; set; }
        [Display(Name = "EDU")]
        public int eduTotal { get; set; }
        public int eduSuccess { get; set; }
    }
}