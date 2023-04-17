using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WIP.Models
{
    public class AuthorModel
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Document Upload")]
        public string DocumentUpload { get; set; }

        [Display(Name = "BooK Description")]
        public string BooKDescription { get; set; }

        [Display(Name = "Number of Authors")]
        public string NumberofAuthors { get; set; }
        public string Remark { get; set; }

    }
}