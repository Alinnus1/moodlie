using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace moodlie.Models
{
    public class Student
    {
        // Id#, Nume, Prenume, AnStudiu
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string Nume { get; set; }
        [Required]
        public string Prenume { get; set; }
        [Required]
        public int AnStudiu { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Curs> Curses { get; set; }
    }
}