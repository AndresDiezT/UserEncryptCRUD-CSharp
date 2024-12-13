using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace UserEncrypt.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Este campo debe tener entre 5 a 50 caracteres.")]
        [Display(Name = "Nombres")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Este campo debe tener entre 5 a 50 caracteres.")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Este campo debe tener entre 5 a 10 caracteres.")]
        [Display(Name = "Numero de identificacion")]
        public string IdentificationNumber { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Este campo debe tener entre 10 a 50 caracteres.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "Este campo debe tener entre 2 a 5 caracteres.")]
        [Display(Name ="Tipo Documento")]
        public string DocumentType { get; set; }
    }
}