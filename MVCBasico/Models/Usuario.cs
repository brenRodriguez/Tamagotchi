using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required (ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "El nombre de usuario debe tener mas de 5 caracteres")]
        public String NombreUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(20, MinimumLength = 8)]
        public String Contrasena { get; set; }

        public ICollection<Mascota> Mascotas { get; set; }
    }
}