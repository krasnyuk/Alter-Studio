using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlterStudio.Models
{
   
    
    public partial class Employees
    {

    
        [Required]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Имя обязательно!")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна!")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Телефон обязателен!")]
        [Display(Name = "Телефон")]
        public string Telephone { get; set; }

        [Display(Name = "Сайт")]
        public string Site { get; set; }

        [Display(Name = "Почта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ForeignKey("Positions")]
        public int PositionId { get; set; }
        [ForeignKey("Cities")]
        public int CityId { get; set; }
    
        [Display(Name = "Имя")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public virtual Cities Cities { get; set; }
        public virtual Positions Positions { get; set; }
       
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
