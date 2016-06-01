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

        [Required(ErrorMessage = "Фамилия обязательна")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Имя обязательна")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Телефон обязателен")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефон")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Email обязателен")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Ставка (%)")]
        [Range(0,100, ErrorMessage = "Ставка должна быть в диапазоне от 0 до 100")]
        public decimal? Rate { get; set; } //Ставка



        [ForeignKey("Cities")]
        public int CityId { get; set; }
        public virtual Cities Cities { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
