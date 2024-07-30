using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Models
{
    //Операции
    public class Operation
    {
        [Key]
        [Display(Name = "Уникальный идентификатор операции")]
        public int OperationId { get; set; }

        [Required]
        [Display(Name = "Идентификатор вкладчика")]
        public int DepositorId { get; set; }
        public Depositor Depositor { get; set; }

        [Required]
        [Display(Name = "Дата вклада")]
        public DateTime DepositDate { get; set; }

        [Display(Name = "Дата возврата")]
        public DateTime? ReturnDate { get; set; }

        [Required]
        [Display(Name = "Идентификатор вклада")]
        public int DepositId { get; set; }
        public Deposit Deposit { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Сумма вклада")]
        public decimal DepositAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Сумма возврата")]
        public decimal? ReturnAmount { get; set; }

        [Required]
        [Display(Name = "Отметка о возврате вклада")]
        public bool IsReturned { get; set; }

        [Display(Name = "Какие сотрудники выполняли операции вкладчика")]
        public ICollection<EmployeeOperation> EmployeeOperations { get; set; }

        [NotMapped]
        public List<int> SelectedEmployeeIds { get; set; }
    }
}
