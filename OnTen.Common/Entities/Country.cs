using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnTen.Common.Entities
{
    public class Country
    {
        public int CountryId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
     

        [DisplayName("Departments Number")]
        public int DepartmentsNumber => Departments == null ? 0 : Departments.Count;


        public ICollection<Department> Departments { get; set; }
    }
}
