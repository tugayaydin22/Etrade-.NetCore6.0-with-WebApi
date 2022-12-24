using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etrade.Entities.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(100)]
        public string? Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Image { get; set; }
        public int Stock { get; set; }
        [Range(0,100000)]
        public decimal Price { get; set; }
        public bool IsHome { get; set; }//Anasayfada mı?
        public bool IsApproved { get; set; }//Satışta mı?
    }
}
