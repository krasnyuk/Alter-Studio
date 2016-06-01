
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlterStudio.Models
{

    
    public partial class Clients
    {
       
    
        [Required]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна!")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Имя обязательно!")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Телефон обязателен!")]
        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Display(Name = "Примечание")]
        public string Note { get; set; }

        [ForeignKey("Cities")]
        public int CityId { get; set; }
    
        public virtual Cities Cities { get; set; }
      
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
