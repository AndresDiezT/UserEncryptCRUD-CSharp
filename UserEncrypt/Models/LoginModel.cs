using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserEncrypt.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Este campo es Obligatorio.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Este campo debe tener entre 6 a 50 caracteres.")]
        [Display(Name = "Nombre Usuario")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Este campo es Obligatorio.")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Este campo debe tener entre 6 a 50 caracteres.")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}