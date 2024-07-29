using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Models
{
    //Валюта
    public class Currency
    {
        [Key]
        [Display(Name = "Уникальный идентификатор валюты")]
        public int CurrencyId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Наименование валюты")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        public ICollection<Deposit> Deposits { get; set; }
        public ICollection<ExchangeRate> ExchangeRates { get; set; }
    }
}
