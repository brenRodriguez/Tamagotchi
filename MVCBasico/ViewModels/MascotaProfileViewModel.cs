using MVCBasico.Models;
using System.Collections.Generic;

namespace Tamagochi.ViewModels
{
    public class MascotaProfileViewModel
    {
        public List<Mascota> listaMascotas { get; set; }
        public Mascota mascotaSeleccionada { get; set; }

    }
}

