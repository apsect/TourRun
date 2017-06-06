using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourRun.Models
{
    public class Transport
    {
        public Transport()
        {
            Tours = new List<Tour>();
        }
        public virtual ICollection<Tour> Tours { get; set; }

        [Required(ErrorMessage = "Виберіть перевізника, якому належать ці транспортні засоби")]
        [Display(Name = "Перевізник - власник")]
        public int? transporterid { get; set; }

        public virtual Transporter Transporter { get; set; }

        public int id { get; set; }

        [Required(ErrorMessage = "Введіть назву транспортного засобу")]
        [StringLength(50)]
        [Display(Name = "Назва транспортного засобу")]
        public string name { get; set; }
    }
}