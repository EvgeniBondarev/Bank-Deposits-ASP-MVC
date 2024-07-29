using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Models
{
    //Вклады
    public class Deposit
    {
        [Key]
        [Display(Name = "Уникальный идентификатор вклада")]
        public int DepositId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Наименование вклада")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Минимальный срок вклада (в месяцах)")]
        public int MinimumTerm { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Минимальная сумма вклада")]
        public decimal MinimumAmount { get; set; }

        [Required]
        [Display(Name = "Идентификатор валюты")]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        [Display(Name = "Процентная ставка")]
        public decimal InterestRate { get; set; }

        [StringLength(500)]
        [Display(Name = "Дополнительные условия")]
        public string AdditionalConditions { get; set; }
    }
}
