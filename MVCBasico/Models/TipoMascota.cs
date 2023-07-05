using Tamagochi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tamagochi.Models
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
    public static long getMaxSinAlimentar(this TipoMascota s1)
    {
        switch (s1)
        {   
            case TipoMascota.Furby:
                return 60 * 60 * 24 * 2; // 2 dia
            case TipoMascota.Gato:
                return 60 * 60 * 24; // 1 dia
            case TipoMascota.Perro:
                return 60 * 60 * 24; // 1 dia
            default:
                return 1000;
        }
    }
    public static string getImagenMascota(this TipoMascota s1)
    {
        string src;

        switch (s1)
        {
            case TipoMascota.Gato:
                src = "/kitty.gif";
                break;
            case TipoMascota.Furby:
                src = "/furby3.gif";
                break;
            case TipoMascota.Perro:
                src = "/doggy.gif";
                break;
            default:
                src = "/furby3.gif";
                break;
        }
        return src;
    }
}