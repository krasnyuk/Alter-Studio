
namespace AlterStudio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class Cities
    {
        [Required]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Название города обязательно!")]
        [Display(Name = "Город")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Название страны обязательно!")]
        [Display(Name = "Страна")]
        public string Country { get; set; }
    
        //navigation properties
        public virtual ICollection<Clients> Clients { get; set; }
    
        public virtual ICollection<Curators> Curators { get; set; }
        
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
