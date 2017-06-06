using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourRun.Models
{
    public class Order
    {
        [Required(ErrorMessage = "Вкажіть тур, який бажаєте замовити")]
        [Display(Name = "Тур")]
        public int? tourid { get; set; }

        public virtual Tour Tour { get; set; }

        public int id { get; set; }

        [Required(ErrorMessage = "Введіть ваше ім'я")]
        [StringLength(150)]
        [Display(Name = "Ім'я")]
        public string name { get; set; }

        [Required(ErrorMessage = "Ввведіть ваше прізвище")]
        [StringLength(150)]
        [Display(Name = "Прізвище")]
        public string surname { get; set; }

        [Required(ErrorMessage = "Введіть ваш номер телефону")]
        [StringLength(15)]
        [Display(Name = "Номер телефону")]
        public string phone { get; set; }

        [Required]
        [StringLength(75)]
        [Display(Name = "Статус")]
        public string status { get; set; }

        [Required(ErrorMessage = "Введіть дату відправлення заявки")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата відправлення заявки")]
        public DateTime orderDate { get; set; }
    }
}