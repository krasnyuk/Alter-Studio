
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlterStudio.Models
{

    
    public class Curators
    {
        [Required]
        public int CuratorId { get; set; }
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия обязательна")]
        public string LastName { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя обязательна")]
        public string FirstName { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Телефон обязателен")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Email обязателен")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Ставка (%)")]
        public decimal? Rate { get; set; }

        [ForeignKey("Cities")]
        public int CityId { get; set; }
        public virtual Cities Cities { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
