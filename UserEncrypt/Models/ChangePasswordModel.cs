using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserEncrypt.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "La contraseña actual es obligatoria.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña Actual")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "La nueva contraseña debe tener entre 6 y 50 caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirma tu nueva contraseña.")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la confirmación no coinciden.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nueva Contraseña")]
        public string ConfirmPassword { get; set; }
    }
}
