using MVCBasico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Models
{
    public enum TipoMascota
    {
        Furby,
        Gato,
        Perro
    }
}
static class MetodosEnum
{

    public static float getMaxSinAlimentar(this TipoMascota s1)
    {
        switch (s1)
        {
            case TipoMascota.Furby:
                return 1000;
            case TipoMascota.Gato:
                return 1000;
            case TipoMascota.Perro:
                return 1000;
            default:
                return 1000;
        }
    }
}