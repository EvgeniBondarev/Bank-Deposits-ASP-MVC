using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Models
{
    //Операции сотрудников
    public class EmployeeOperation
    {
        [Key]
        [Display(Name = "Уникальный идентификатор операции сотрудника")]
        public int EmployeeOperationId { get; set; }

        [Required]
        [Display(Name = "Идентификатор сотрудника")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        [Display(Name = "Идентификатор операции")]
        public int OperationId { get; set; }
        public Operation Operation { get; set; }
    }
}
