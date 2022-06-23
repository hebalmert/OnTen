using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OnTen.Common.Helpers
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);

    }
}
