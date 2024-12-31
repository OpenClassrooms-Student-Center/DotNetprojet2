using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P2FixAnAppDotNetCode.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        public string Name { get; set; }

        [Required(ErrorMessage = "L'adresse est obligatoire")]
        public string Address { get; set; }

        [Required(ErrorMessage = "La ville est obligatoire")]
        public string City { get; set; }

        [Required(ErrorMessage = "Zip code obligatoire")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Le pays est obligatoire")]
        public string Country { get; set; }

        [BindNever]
        public DateTime Date { get; set; }
    }
}
