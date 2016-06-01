using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlterStudio.Models
{
    public partial class Orders : IValidatableObject
    {

        [Required]
        [Display(Name = "Номер")]
        public int OrderId { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Описание обязательно")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Дата съёмки обязательна")]
        [Display(Name = "Дата съёмки")]
        public DateTime ShootingDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Сдать до")]
        public DateTime? PerformTo { get; set; }

        [Required(ErrorMessage = "Куратор обязателен")]
        [ForeignKey("Curators")]
        public int CuratorId { get; set; }

        [Required(ErrorMessage = "Клиент обязателен")]
        [ForeignKey("Clients")]
        public int ClientId { get; set; }

        public virtual Clients Clients { get; set; }
        public virtual Curators Curators { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PerformTo < ShootingDate)
            {
                yield return new ValidationResult("Дата съёмки должна быть раньше даты сдачи материала");
            }
        }
    }
}
