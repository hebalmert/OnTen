using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace OnTen.Common.Entities
{
    public class Country
    {
        public int CountryId { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Country")]
        public string Name { get; set; }
     

        [DisplayName("Departments Number")]
        public int DepartmentsNumber => Departments == null ? 0 : Departments.Count;

       
        public ICollection<Department> Departments { get; set; }
    }
}
