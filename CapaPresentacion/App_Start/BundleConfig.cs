﻿using System.Web;
using System.Web.Optimization;

namespace CapaPresentacion
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new Bundle("~/bundles/funcionesjs").Include(
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.responsive.js",
                        "~/Scripts/loadingoverlay/loadingoverlay.min.js",
                        "~/Scripts/sweetalert.min.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/fontawesome/all.min.js",
                        "~/Scripts/jquery-ui-1-14.1.js",
                        "~/Scripts/jquery-ui-1.14.1.js",
                        "~/Scripts/jquery-ui-1.14.1.min.js",
                        "~/Scripts/sb-admin-2.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //"~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //"~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/sb-admin-2.min.css",
                      "~/Content/Site.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/font-awesome.css",
                      "~/Content/DataTables/css/jquery.dataTables.css",
                      "~/Content/DataTables/css/responsive.dataTables.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/jquery-ui.min.css",
                      "~/Content/sweetalert.css",

                      "~/vendor/fontawesome-free/css/all.min.css"));

            
        }
    }
}
