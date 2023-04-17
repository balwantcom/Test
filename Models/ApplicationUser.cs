using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WIP.Models
{
    public class ApplicationUser
    {
        public string Email { get; set; }
        public bool ConfirmedEmail { get; set; }

    }
}