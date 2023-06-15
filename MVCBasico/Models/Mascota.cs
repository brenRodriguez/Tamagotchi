using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Tamagochi.Models;

namespace MVCBasico.Models
{
    public class Mascota
    {
        public Mascota(string nombreMascota, TipoMascota tipoDeMascota, int userID)
        {
            NombreMascota = nombreMascota;
            TipoDeMascota = tipoDeMascota;
            UserID = userID;
            UltimaVezAlimentado = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            TiempoMaximoSinAlimentar = TipoDeMascota.getMaxSinAlimentar();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Nombre Mascota")]
        public String NombreMascota { get; set; }
        
        [EnumDataType(typeof(TipoMascota))]
        [Display(Name = "Tipo de Mascota")]
        public TipoMascota TipoDeMascota { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public Usuario Usuario { get; set; }

        [Required]
        public long UltimaVezAlimentado {get; set;}

        [Required]
        public long TiempoMaximoSinAlimentar {get; set;}

        // enum
        public Estado Estado {
            get
            {
                return calcularEstado();
            } 
            //set
            //{

            //}
         }

        private Estado calcularEstado()
        {
            long tiempoDesdeAlimentado = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            if (this.)
                
            return Estado.FELIZ;
        }

    }
}