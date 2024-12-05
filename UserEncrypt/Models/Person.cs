using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserEncrypt.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Este campo debe tener entre 4 a 50 caracteres.")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Este campo debe tener entre 4 a 50 caracteres.")]
        public string Apellido { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Este campo debe tener maximo 10 caracteres.")]
        public string NumeroIdentificacion { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "Este campo debe tener entre 9 a 50 caracteres")]
        public string Email { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public int TipoDocumentoId { get; set; }
       
    }
}
