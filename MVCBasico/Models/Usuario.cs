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
        // Lo que está entre [ ] se llaman anotaciones, y "decoran" los atributos

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }

        //[Required(ErrorMessage = "El nombre es obligatorio")]
        //public string Nombre { get; set; }
        //[Required(ErrorMessage = "La contraseña es obligatoria")]
        //public string Contrasena { get; set; }

        //public Mascota Mascota { get; set; }

        //[Display(Name = "Fecha inscripción")]   //indica que mostrará el label de la página HTML
        //public DateTime FechaInscripto { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required]
        public String NombreUsuario { get; set; }

        [Required]
        public String Contrasena { get; set; }

        public ICollection<Mascota> Mascotas { get; set; }
    }
}