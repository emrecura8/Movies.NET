using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Movies.NET.Controllers.Bases
{
    public abstract class MVCControllerBase :Controller
    {
        protected   MVCControllerBase() 
        {
            CultureInfo cultureInfo =new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}
