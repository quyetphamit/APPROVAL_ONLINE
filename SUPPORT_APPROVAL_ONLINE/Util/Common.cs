using SUPPORT_APPROVAL_ONLINE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SUPPORT_APPROVAL_ONLINE.Util
{
    public static class Common
    {
        public enum StatusApproval
        {
            NY, NA, OK
        }
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
                obj.requestor = r.tbl_User.fullname;
                obj.dept = r.tbl_User.tbl_Group.group_Name;
                obj.deptMNG = r.date_Dept_MNG_Approval != null;

                obj.lcaLeader = r.date_LCA_Leader_Approval != null;

                obj.lcaMNG = r.date_LCA_MNG_Approval != null;

                obj.requestorApproval = r.date_Requestor_Approval != null;

                obj.deptMNGConfirm = r.date_Dept_Confirm != null;

                //obj.comtor = r.date_Comtor_Approval != null;
                if (r.date_Comtor_Approval != null)
                {
                    obj.comtor = (int)StatusApproval.OK;
                }
                else if (r.U_Id_Approval == 1 || r.U_Id_Comtor == -1)
                {
                    obj.comtor = (int)StatusApproval.NA;
                }
                else
                {
                    obj.comtor = (int)StatusApproval.NY;
                }
                //obj.fm = r.date_FM_Approval != null;
                if (r.date_FM_Approval != null)
                {
                    obj.fm = (int)StatusApproval.OK;
                }
                else if (r.U_Id_Approval == 1)
                {
                    obj.fm = (int)StatusApproval.NA;
                }
                else
                {
                    obj.fm = (int)StatusApproval.NY;
                }
                if (r.date_GD_Approval != null)
                {
                    obj.gd = (int)StatusApproval.OK;
                }
                else if (r.U_Id_Approval == 1)
                {
                    obj.gd = (int)StatusApproval.NA;
                }
                else
                {
                    obj.gd = (int)StatusApproval.NY;
                }
                //obj.gd = r.date_GD_Approval != null;
                result.Add(obj);
            });
            return result;
        }
        public static void SendMail(string mailTo, string name, bool isSend)
        {
            // Send mail
            var message = new MailMessage();
            message.To.Add(new MailAddress(mailTo));
            message.From = new MailAddress(CONST.MAIL);
            message.Subject = CONST.SUBJECT;
            message.Body = isSend ? CONST.BODY_SEND : CONST.BODY_RECEI + name;
            message.IsBodyHtml = false;
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = CONST.MAIL,
                    Password = CONST.PASS
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(message);
            }
        }
    }
}