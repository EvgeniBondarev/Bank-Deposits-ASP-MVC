using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Models
{
    //Курсы валют
    public class ExchangeRate
    {
        [Key]
        [Display(Name = "Уникальный идентификатор курса валют")]
        public int ExchangeRateId { get; set; }

        [Required]
        [Display(Name = "Дата курса")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Идентификатор валюты")]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        [Display(Name = "Стоимость")]
        public decimal Rate { get; set; }
    }
}
