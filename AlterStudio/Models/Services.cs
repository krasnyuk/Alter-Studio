
using System.ComponentModel.DataAnnotations;

namespace AlterStudio.Models
{
    using System;
    using System.Collections.Generic;
    
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
        public int Cost { get; set; }
        [Display(Name = "Примечание")]
        public string Note { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
