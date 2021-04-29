﻿using System.Web;
using System.Web.Optimization;

namespace FYPManagment
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootbox.js",
                "~/Scripts/typeahead.bundle.js",
                "~/Scripts/toastr.js",
                 "~/Scripts/jquery.signalR-2.4.1.min.js",
                 "~/Scripts/moment.min.js",
                "~/Scripts/underscore-min.js"
               
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Typeahead.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css",
                      "~/Content/animate.css",
                      "~/Content/CustomStyling.css"));
        }
    }
}
