using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVCBasico.Models
{
    public class Mascota
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string NombreMascota { get; set; }

        [Required]
        public int UltimaVezAlimentado { get; set; }
        [Required]
        public int TiempoMaximoSinAlimentar { get; set; }

        [EnumDataType(typeof(TipoMascota))]
        public TipoMascota TipoDeMascota { get; set; }

        [Required]
        public int UserID { get; set; }
        [Required]
        public Usuario Usuario { get; set; }

    }
}