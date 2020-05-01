using System.Web;
using System.Web.Optimization;

namespace VentaTicketsUnicornio
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/assets/bootstrap/css").Include(
            "~/assets/bootstrap/css/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/assets/css").Include(
                        "~/assets/css/Article-Clean.css",
                        "~/assets/css/Login-Form-Clean.css"));

            bundles.Add(new StyleBundle("~/assets/img").Include(
                    "~/assets/img/g4912.png"));
        }
    }
}
