using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace OnTen.Web.Helper
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboCategories();

        IEnumerable<SelectListItem> GetComboCountries();

        IEnumerable<SelectListItem> GetComboDepartments(int countryId);

        IEnumerable<SelectListItem> GetComboCities(int departmentId);

        IEnumerable<SelectListItem> GetOrderStatuses();
    }
}