using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace moodlie.Models
{
    public class Curs
    {
        [Key]     
        public int CursId { get; set; }
        [Required]
        public string Nume { get; set; }

        public int ProfesorId { get; set; }

        public virtual Profesor ProfesorTitular { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Section> Sections { get; set; }

        public IEnumerable<SelectListItem> Profesors { get; set; }
    }
}