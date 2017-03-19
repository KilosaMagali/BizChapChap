using System.Web.Optimization;

namespace BizChapChap.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/vendors/jquery-1.10.2.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new Bundle("~/bundles/modernizr").Include(
                        "~/Scripts/vendors/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/vendors/bootstrap.js",
                      "~/Scripts/vendors/respond.js"));

            bundles.Add(new Bundle("~/bundles/angularjs").Include(
                      "~/Scripts/vendors/angular.js",
                      "~/Scripts/vendors/angular-route.js",
                      "~/Scripts/vendors/ng-file-upload.js"));

            bundles.Add(new Bundle("~/bundles/angularcss").Include(
                      "~/Scripts/vendors/angular-css.js"));


            bundles.Add(new Bundle("~/bundles/spa").Include(
                "~/Scripts/spa/app.js",
                "~/Scripts/spa/account/registerCtrl.js",
                "~/Scripts/spa/home/rootCtrl.js",
                "~/Scripts/spa/home/indexCtrl.js",
                "~/Scripts/spa/services/apiService.js",
                "~/Scripts/spa/services/fileUploadService.js",
                "~/Scripts/spa/account/loginCtrl.js",
                "~/Scripts/spa/upload/fileUploadCtrl.js",
                "~/Scripts/spa/listings/listingAddCtrl.js"
                ));

           

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
