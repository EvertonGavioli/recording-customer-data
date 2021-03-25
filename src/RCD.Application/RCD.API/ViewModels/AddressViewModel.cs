using System;
using System.ComponentModel.DataAnnotations;

namespace RCD.API.ViewModels
{
    public class AddressViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public string Number { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public string City { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public string State { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public string Country { get; set; }
    }
}
