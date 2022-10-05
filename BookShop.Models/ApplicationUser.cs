using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        [Required]
        public string Name { get; set; } 

        public string? StreetAddress { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? PostalCode { get; set; }

        public int? CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }
        
    }
}