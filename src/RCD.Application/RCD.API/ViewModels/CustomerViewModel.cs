using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RCD.API.ViewModels
{
    public class BaseCustomerViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public string Email { get; set; }
    }

    public class CustomerViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        public string Email { get; set; }

        public ICollection<PhoneViewModel> Phones { get; set; }

        public ICollection<AddressViewModel> Addresses { get; set; }
    }
}
