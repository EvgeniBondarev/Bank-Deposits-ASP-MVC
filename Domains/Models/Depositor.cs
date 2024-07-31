using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Models
{
    //Вкладчики
    public class Depositor
    {
        [Key]
        [Display(Name = "Уникальный идентификатор вкладчика")]
        public int DepositorId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "ФИО вкладчика")]
        public string FullName { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Адрес вкладчика")]
        public string Address { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Телефон вкладчика")]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Паспортные данные вкладчика")]
        public string PassportDetails { get; set; }

        [Display(Name = "Операции вкладчика вкладчика")]
        public ICollection<Operation> Operations { get; set; }
    }
}
