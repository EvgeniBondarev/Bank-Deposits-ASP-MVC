using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Models
{
    //Сотрудники
    public class Employee
    {
        [Key]
        [Display(Name = "Уникальный идентификатор сотрудника")]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "ФИО сотрудника")]
        public string FullName { get; set; }

        [StringLength(100)]
        [Display(Name = "Должность сотрудника")]
        public string Position { get; set; }

        public ICollection<EmployeeOperation> EmployeeOperations { get; set; }
    }
}
