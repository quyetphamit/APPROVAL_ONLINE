using SUPPORT_APPROVAL_ONLINE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SUPPORT_APPROVAL_ONLINE.Util
{
    public static class Common
    {
        public static string EncryptionMD5(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(input);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }
        /// <summary>
        /// Mapping đối tượng hiện thị màn hình chính, cho biết trạng thái của yêu cầu
        /// </summary>
        /// <param name="lstRequest">Danh sách yêu cầu</param>
        /// <returns>Danh sách trạng thái của yêu cầu</returns>
        public static List<RequestStatus> Mapping(List<tbl_Request> lstRequest)
        {
            List<RequestStatus> result = new List<RequestStatus>();
            lstRequest = lstRequest.OrderByDescending(r => r.id).ToList();
            lstRequest.ForEach(r =>
            {
                RequestStatus obj = new RequestStatus();
                obj.IdOrigin = r.id;
                obj.id = r.date_Create.Year + "-LCA-RQ" + r.id.ToString("000");
                obj.title = r.title;
                obj.requestor = r.tbl_User.U_fullname;
                obj.deptMNG = r.U_Id_LCA_Leader != null;
                obj.lcaLeader = r.U_Id_LCA_MNG != null;
                obj.lcaMNG = r.U_Id_Approval == r.U_Id_Dept_MNG;
                obj.deptMNGConfirm = r.U_Id_FM != null;
                obj.fm = r.U_Id_GD != null;
                obj.gd = false;
                result.Add(obj);
            });
            return result;
        }
    }
}