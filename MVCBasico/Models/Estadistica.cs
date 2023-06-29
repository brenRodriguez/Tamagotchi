using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Tamagochi.Models;
using System;

namespace Tamagochi.Models
{
    public class Estadistica
    {
        public Estadistica()
        {
            TiempoHambrento = 0;
            TiempoDebil = 0;
            UltimaActualizacion = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [ForeignKey("MascotaTrackeada")]
        public int MascotaId { get; set; }

        [Required]
        public Mascota MascotaTrackeada{ get; set; }

        [Required]
        public long TiempoHambrento { get; set; }

        [Required]
        public long TiempoDebil { get; set; }

        [Required]
        public long UltimaActualizacion { get; set; }

        public long TiempoSatisfecho { 
            get 
            { 
                return getTiempoSatisfecho(); 
            }
        }


        public void actualizarEstadistica()
        {
            long tiempoDesdeAlimentado = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - MascotaTrackeada.UltimaVezAlimentado;

            if (tiempoDesdeAlimentado > this.MascotaTrackeada.TiempoMaximoSinAlimentar )
            {
                TiempoDebil += tiempoDesdeAlimentado - (MascotaTrackeada.TiempoMaximoSinAlimentar);
                TiempoHambrento += MascotaTrackeada.TiempoMaximoSinAlimentar / 2;

            } else if (tiempoDesdeAlimentado > MascotaTrackeada.TiempoMaximoSinAlimentar / 2)
            {
                TiempoHambrento += tiempoDesdeAlimentado;
            }
        }

        private long getTiempoSatisfecho()
        {
            // Tiempo actual - tiempo de creacion (tiempo de vida) - (tiempo que estuvo debil + tiempo que estuvo hambriento)
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() - (MascotaTrackeada.TiempoDeCreacion - (TiempoDebil + TiempoHambrento));
        }
    }

  
}
