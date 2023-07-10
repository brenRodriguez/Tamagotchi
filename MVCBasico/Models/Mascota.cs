using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Tamagochi.Models;

namespace Tamagochi.Models
{
    public class Mascota
    {
        public Mascota(string nombreMascota, TipoMascota tipoDeMascota, int userID)
        {

            long tiempoActual = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            NombreMascota = nombreMascota;
            TipoDeMascota = tipoDeMascota;
            UserID = userID;
            UltimaVezAlimentado = tiempoActual;
            TiempoDeCreacion = tiempoActual;
            TiempoMaximoSinAlimentar = TipoDeMascota.getMaxSinAlimentar();
            Estadisticas = new Estadistica();
            Estadisticas.MascotaTrackeada = this;
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

        [Required]
        public long TiempoDeCreacion { get; set; }

        [Required]
        public Estadistica Estadisticas { get; set; }

        // enum
        public Estado Estado {
            get
            {
                return calcularEstado();
            }
         }

        public void actualizarEstado()
        {
            this.Estadisticas.actualizarEstadistica();
        }

        private Estado calcularEstado()
        {           
            long tiempoDesdeAlimentado = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - this.UltimaVezAlimentado;

            // si supera el tiempo max que aguanta la mascota sin alimentar esta está debil
            if (tiempoDesdeAlimentado > this.TiempoMaximoSinAlimentar)
            {
                return Estado.DEBIL;
            // en cambio, si el tiempo desde alimentado es menor que el maximo pero mayor que la mitad de este, la mascota esta hambrienta
            } else if (tiempoDesdeAlimentado > this.TiempoMaximoSinAlimentar / 2)
            {
                return Estado.HAMBRIENTO;
            } else
            {
                return Estado.SATISFECHO;
            }
        }
    }
}