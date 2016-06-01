using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlterStudio.Models
{
    public class Services
    {
        [Required]
        public int ServiceId { get; set; }
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Название является обязательным!")]
        public string Title { get; set; }
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Описание является обязательным!")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Стоимость является обязательным!")]
        [Range(0,99999, ErrorMessage = "Число должно быть в диапазоне 0 - 99999")]
        public decimal Cost { get; set; }
        [Display(Name = "Примечание")]
        public string Note { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
