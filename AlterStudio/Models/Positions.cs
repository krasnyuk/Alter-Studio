using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlterStudio.Models
{
    public class Positions
    {
        [Required]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "Название обязательно!")]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Описание обязательно!")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Примечание")]
        public string Note { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
