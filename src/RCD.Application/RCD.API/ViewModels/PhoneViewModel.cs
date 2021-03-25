using RCD.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RCD.API.ViewModels
{
    public class PhoneViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public PhoneType PhoneType { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public string PhoneNumber { get; set; }
    }
}
