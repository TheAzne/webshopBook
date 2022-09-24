using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Cover type")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

    }
}