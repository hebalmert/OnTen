using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace OnTen.Common.Entities
{
    public class City
    {
        public int CityId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public int DepartmentId { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Department Department { get; set; }
    }
}
