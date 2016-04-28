

namespace AlterStudio.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class Clients
    {
        [Required]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Имя обязательно!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Телефон обязателен!")]
        public string Telephone { get; set; }
        public string Note { get; set; }


        public int CityId { get; set; }
    
        public virtual Cities Cities { get; set; }
       
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
