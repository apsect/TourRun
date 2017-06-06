using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourRun.Models
{
    public class Tour
    {
        public Tour()
        {
            Pictures = new List<Picture>();
            Hotels = new List<Hotel>();
            Transports = new List<Transport>();
            Orders = new List<Order>();
        }

        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
        public virtual ICollection<Transport> Transports { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        //main
        public int id { get; set; }

        [Required(ErrorMessage = "Введіть назву")]
        [StringLength(50)]
        [Display(Name = "Назва туру")]
        public string name { get; set; }

        [Required(ErrorMessage = "Введіть маршрут")]
        [StringLength(240)]
        [Display(Name = "Маршрут")]
        public string route { get; set; }

        [Required(ErrorMessage = "Введіть тип харчування")]
        [StringLength(50)]
        [Display(Name = "Тип харчування")]
        public string eat { get; set; }

        [Required(ErrorMessage = "Введіть кількість місць")]
        [Range(1, 100, ErrorMessage = "Ми не можемо забезпечити тур на стільки людей")]
        [Display(Name = "Кількість місць")]
        public int places { get; set; }

        [Required(ErrorMessage = "Введіть дату відправлення")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата відправлення")]
        public DateTime departure { get; set; }

        [Required(ErrorMessage = "Введіть тривалість")]
        [Range(1, 180, ErrorMessage = "Для стількох днів інша процедура оформлення")]
        [Display(Name = "Тривалість подорожі")]
        public int duration { get; set; }

        [Required(ErrorMessage = "Введіть ціну подорожі")]
        [Range(10, 10000, ErrorMessage = "Ми з такими сумами не працюємо")]
        [DisplayFormat(DataFormatString = "{0} $")]
        [Display(Name = "Ціна подорожі")]
        public int cost { get; set; }

        //additional
        [Required(ErrorMessage = "Введіть план подорожі")]
        [StringLength(14000)]
        [Display(Name = "План подорожі")]
        public string plan { get; set; }

        [Required(ErrorMessage = "Введіть додаткову інформацію")]
        [StringLength(5000)]
        [Display(Name = "Додаткова інформація")]
        public string description { get; set; }
    }
}