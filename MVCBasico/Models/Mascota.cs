using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace MVCBasico.Models
{
    public class Mascota
    {
        private TipoMascota tipoMascota;

        public Mascota(string nombreMascota, TipoMascota tipoMascota, Usuario usuario)
        {
            NombreMascota = nombreMascota;
            TipoDeMascota = tipoMascota;
            Usuario = usuario;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string NombreMascota { get; set; }
        [EnumDataType(typeof(TipoMascota))]
        public TipoMascota TipoDeMascota { get; set; }

        [Required]
        public Usuario Usuario { get; set; }

        [Required]
        public long UltimaVezAlimentado {
            get
            {
                return this.UltimaVezAlimentado;
            } set
            {
                this.UltimaVezAlimentado = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }
        }

        [Required]
        public float TiempoMaximoSinAlimentar
        {
            get
            {
                return this.TiempoMaximoSinAlimentar;
            }
            set
            {
                this.TiempoMaximoSinAlimentar = this.TipoDeMascota.getMaxSinAlimentar();
            }
        }     

    

    }
}