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
            long tiempoActual = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            long tiempoDesdeAlimentado = tiempoActual - MascotaTrackeada.UltimaVezAlimentado;
            long tiempoDesdeActualizacion = tiempoActual - UltimaActualizacion;
 
            // Resto el tiempo desde la ultima actualizacion para que no se cuenten segundos mas de una vez
            if (tiempoDesdeAlimentado > tiempoDesdeActualizacion)
            {
                tiempoDesdeAlimentado -= tiempoDesdeActualizacion;
            }

            // el tiempo que no se agrega a ninguna variable es por defecto tiempo satisfecho
            if (tiempoDesdeAlimentado > this.MascotaTrackeada.TiempoMaximoSinAlimentar)
            {
                TiempoHambrento += MascotaTrackeada.TiempoMaximoSinAlimentar / 2;
                TiempoDebil += tiempoDesdeAlimentado - (MascotaTrackeada.TiempoMaximoSinAlimentar);

            } else if (tiempoDesdeAlimentado > MascotaTrackeada.TiempoMaximoSinAlimentar / 2)
            {
                TiempoHambrento += tiempoDesdeAlimentado - MascotaTrackeada.TiempoMaximoSinAlimentar / 2;
            }

            UltimaActualizacion = tiempoActual;
        }

        private long getTiempoSatisfecho()
        {
           // Tiempo actual - tiempo de creacion (tiempo de vida) - (tiempo que estuvo debil + tiempo que estuvo hambriento)

            var tiempoDeVida = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - MascotaTrackeada.TiempoDeCreacion;
            return tiempoDeVida - (TiempoDebil + TiempoHambrento);
        }

        
        public double[] secsToTiempo(float secs)
        {
            int dia = 60 * 60 * 24;
            int hora = 60 * 60;
            int minuto = 60;

            // divido la cantidad de segundos totales por la cantidad de segundos en un dia y le remuevo el decimal
            var diaOut = Math.Floor(secs / dia); 

            // segundos totales - (cantidad de dias * segundos en un dia) y le remuevo el decimal
            var horaOut = Math.Floor((secs - diaOut * dia) / hora); 

            // segundos totales - (cantidad de dias * segundos en un dia) - (cant horas * segundos en una hora)
            var minutoOut = Math.Floor((secs - diaOut * dia - horaOut * hora) / minuto); 

            // segundos totales - (cantidad de dias * segundos en un dia) - (cant horas * segundos en una hora) - (cant minutos * segs en un minuto)
            var segundoOut = secs - diaOut * dia - horaOut * hora - minutoOut * minuto; 

            double[] result = { diaOut, horaOut, minutoOut, segundoOut };

            return  result;
        }
    }
}
