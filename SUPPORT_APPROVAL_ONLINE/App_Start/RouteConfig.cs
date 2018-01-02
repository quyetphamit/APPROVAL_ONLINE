using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SUPPORT_APPROVAL_ONLINE
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Request",
              url: "yeu-cau/tao-moi/",
              defaults: new { controller = "Request", action = "Create" }
          );

            routes.MapRoute(
             name: "RequestList",
             url: "yeu-cau/danh-sach-yeu-cau/",
             defaults: new { controller = "Request", action = "Index" }
         );
            routes.MapRoute(
            name: "RequestDetails",
            url: "yeu-cau/chi-tiet/{requestId}",
            defaults: new { controller = "Request", action = "Details",requestId = UrlParameter.Optional }
        );

            routes.MapRoute(
            name: "RequestApproval",
            url: "yeu-cau/danh-sach-can-phe-duyet",
            defaults: new { controller = "Business", action = "getRequestByUserApproval", requestId = UrlParameter.Optional }
        );

            routes.MapRoute(
            name: "RequestCreated",
            url: "yeu-cau/danh-sach-da-tao",
            defaults: new { controller = "Business", action = "getRequestByUserCreate", requestId = UrlParameter.Optional }
        );

            routes.MapRoute(
            name: "SearchRequest",
            url: "tim-kiem/phong-ban/{deptName}",
            defaults: new { controller = "Business", action = "getByDept", deptName = UrlParameter.Optional }
        );

            routes.MapRoute(
            name: "ChangePassword",
            url: "nguoi-dung/doi-mat-khau",
            defaults: new { controller = "User", action = "ChangePassword", deptName = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "Chart",
            url: "Trang-chu/thong-ke-theo-phong-ban",
            defaults: new { controller = "Home", action = "Chart", deptName = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "Home",
            url: "Trang-chu",
            defaults: new { controller = "Home", action = "Index", deptName = UrlParameter.Optional }
        );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}
