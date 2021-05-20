using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace moodlie.Models
{
    public class Section
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Titlul sectiunii este obligatoriu")]
        public string Titlu { get; set; }

        public int CursId { get; set; }
        public virtual Curs Curs { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}