using System.ComponentModel.DataAnnotations;

namespace TourRun.Models
{
    public class Picture
    {
        [Required(ErrorMessage = "Виберіть тур, якому будуть доступні ці фото")]
        [Display(Name = "Тур - власник")]
        public int? tourid { get; set; }

        public virtual Tour Tour { get; set; }

        public int id { get; set; }

        [Required(ErrorMessage = "Введіть шлях до зображення")]
        [Display(Name = "Шлях до зображення")]
        public string imagePath { get; set; }
    }
}