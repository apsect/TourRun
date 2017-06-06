using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourRun.Models
{
    public class Hotel
    {
        public Hotel()
        {
            Tours = new List<Tour>();
        }
        public virtual ICollection<Tour> Tours { get; set; }
        public int id { get; set; }

        [Required(ErrorMessage = "Введіть назву готелю")]
        [StringLength(50)]
        [Display(Name = "Назва")]
        public string name { get; set; }

        [Required(ErrorMessage = "Встановіть \"зірковість\" готелю")]
        [Range(1, 5, ErrorMessage = "Не існує готелів з такою кількістю зірок")]
        [Display(Name = "Кількість зірок")]
        public int stars { get; set; }

        [Required(ErrorMessage = "Вкажіть тип кімнат")]
        [StringLength(50)]
        [Display(Name = "Тип кімнат")]
        public string roomType { get; set; }
    }
}