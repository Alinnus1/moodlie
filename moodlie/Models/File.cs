using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace moodlie.Models
{
    public class File
    {
        [Key]
        public int FileId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public DateTime UploadTime { get; set; }
        public int SectionId { get; set; }
        public virtual Section Section { get; set; }
    }
}