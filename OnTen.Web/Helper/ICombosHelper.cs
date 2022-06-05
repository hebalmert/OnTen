using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace OnTen.Web.Helper
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboCategories();


    }
}