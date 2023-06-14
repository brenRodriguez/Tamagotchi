using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace MVCBasico.Models
{
    public class Mascota
    {
        public Mascota(string nombreMascota, TipoMascota tipoDeMascota, int userID)
        {
            NombreMascota = nombreMascota;
            TipoDeMascota = tipoDeMascota;
            UserID = userID;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public String NombreMascota { get; set; }
        [EnumDataType(typeof(TipoMascota))]
        public TipoMascota TipoDeMascota { get; set; }

        [Required]
        public int UserID { get; set; }
        [Required]
        public Usuario Usuario { get; set; }

        

        [Required]
        public long UltimaVezAlimentado 
        { 
            get { return UltimaVezAlimentado; }
            set { this.UltimaVezAlimentado = DateTimeOffset.UtcNow.ToUnixTimeSeconds(); }
        }



        [Required]
        public float TiempoMaximoSinAlimentar
        {
            get { return TiempoMaximoSinAlimentar; }
            set { this.TiempoMaximoSinAlimentar = this.TipoDeMascota.getMaxSinAlimentar(); }
        }     
    }
}