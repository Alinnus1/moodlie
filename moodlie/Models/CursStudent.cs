using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace moodlie.Models
{
    public class CursStudent
    {
        [Key]
        public int CSId { get; set; }
        public int CursId { get; set; }
        public int StudentId { get; set; }
    }
}