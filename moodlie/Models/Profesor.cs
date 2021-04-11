using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace moodlie.Models
{
    public class Profesor
    {
        //Id#, Nume, Prenume, GradDidactic
        [Key]
        public int ProfesorId { get; set; } 
        [Required]
        public string Nume  { get; set; }
        [Required]
        public string Prenume { get; set; }
        [Required]
        public string GradDidactic { get; set; }

        public string UserId { get; set; }
    }
}