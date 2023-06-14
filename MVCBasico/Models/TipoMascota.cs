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
                return 60 * 60 * 24; // 1 dia
            case TipoMascota.Gato:
                return 60 * 60 * 12; // 12 hs
            case TipoMascota.Perro:
                return 60 * 60 * 12; // 12 hs
            default:
                return 1000;
        }
    }
}