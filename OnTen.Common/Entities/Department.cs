using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace OnTen.Common.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }

        [DisplayName("Cities Number")]
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;


        public int CountryId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Country Country { get; set; }
    }
}
