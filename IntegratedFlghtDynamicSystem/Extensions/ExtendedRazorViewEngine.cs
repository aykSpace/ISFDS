using System.Collections.Generic;
using System.Web.Mvc;

namespace IntegratedFlghtDynamicSystem.Extensions
{
    public class ExtendedRazorViewEngine : RazorViewEngine
    {
        public void AddViewLocationFormat(string path)
        {
            var existingPaths = new List<string>(ViewLocationFormats) {path};
            ViewLocationFormats = existingPaths.ToArray();
        }

        public void AddPartialViewLocationFormat(string path)
        {
            var existingPaths = new List<string>(PartialViewLocationFormats) { path };
            PartialViewLocationFormats = existingPaths.ToArray();
        }
    }
}