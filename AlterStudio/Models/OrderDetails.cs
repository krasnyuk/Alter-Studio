using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlterStudio.Models
{

    public partial class OrderDetails
    {
        [Column("ServiceId", Order = 1)]
        [Required]
        [Display(Name = "Услуга")]
        [ForeignKey("Services")]
        public int ServiceId { get; set; }

        [Column("OrderId", Order = 2)]
        [Required]
        [Display(Name = "Заказ")]
        [ForeignKey("Orders")]
        public int OrderId { get; set; }

        [Display(Name = "Количество")]
        [Range(0,int.MaxValue,ErrorMessage = "Количество не может быть отрицательным")]
        public int? Amount { get; set; }

        [Required]
        [ForeignKey("Employees")]
        public int EmployeeId { get; set; }
    
        public virtual Employees Employees { get; set; }
        public virtual Orders Orders { get; set; }
        public virtual Services Services { get; set; }
    }
}
