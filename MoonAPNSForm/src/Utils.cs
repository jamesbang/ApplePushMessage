using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonAPNSForm.src
{
    class Utils
    {
        public static string getNowTime() {
            DateTime localDate = DateTime.Now;
            var culture = new CultureInfo("en-US");
            return localDate.ToString(culture);
        }
    }
}
