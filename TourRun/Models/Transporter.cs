using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TourRun.Models
{
    public class Transporter
    {
        public Transporter()
        {
            Tours = new List<Tour>();
            Transports = new List<Transport>();
        }
        public virtual ICollection<Tour> Tours { get; set; }
        public virtual ICollection<Transport> Transports { get; set; }

        public int id { get; set; }

        [Required(ErrorMessage = "Введіть назву компанії перевізника")]
        [StringLength(50)]
        [Display(Name = "Назва компанії")]
        public string name { get; set; }
    }
}