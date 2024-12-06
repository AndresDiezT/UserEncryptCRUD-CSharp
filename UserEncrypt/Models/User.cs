using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserEncrypt.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Este campo debe tener entre 6 a 50 caracteres.")]
        [Display(Name = "Nombre Usuario")]
        public string Username { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Este campo debe tener entre 6 a 50 caracteres.")]
        [Display(Name = "Contraseña")]
        public string PasswordHash { get; set; }
        [HiddenInput]
        public byte[] HashKey { get; set; }
        [HiddenInput]
        public byte[] HashIV { get; set; }
        [HiddenInput]
        public DateTime CreatedAt { get; set; }
    }
}