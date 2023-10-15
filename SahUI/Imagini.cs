using LogicaJoc;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SahUI
{
    public static class Imagini
    {

        // Incarca imaginile din resurse si le face usor de accesat

        private static readonly Dictionary<TipPiesa, ImageSource> SursaA = new()
        {

             {TipPiesa.Pion, LoadImage("Resurse/PionA.png")},
             {TipPiesa.Tura, LoadImage("Resurse/TuraA.png")},
             {TipPiesa.Cal, LoadImage("Resurse/CalA.png")},
             {TipPiesa.Nebun, LoadImage("Resurse/NebunA.png")},
             {TipPiesa.Regina, LoadImage("Resurse/ReginaA.png")},
             {TipPiesa.Rege, LoadImage("Resurse/RegeA.png")}

        };

        private static readonly Dictionary<TipPiesa, ImageSource> SursaN = new()
        {

             {TipPiesa.Pion, LoadImage("Resurse/PionN.png")},
             {TipPiesa.Tura, LoadImage("Resurse/TuraN.png")},
             {TipPiesa.Cal, LoadImage("Resurse/CalN.png")},
             {TipPiesa.Nebun, LoadImage("Resurse/NebunN.png")},
             {TipPiesa.Regina, LoadImage("Resurse/ReginaN.png")},
             {TipPiesa.Rege, LoadImage("Resurse/RegeN.png")}

};

        public static ImageSource LoadImage(string filePath) //incarca locatia unei imagini cape un parametru
        {

            return new BitmapImage(new Uri(filePath, UriKind.Relative));

        }

        // Preia culoarea si tipul piesei si retunieaza imaginea corespunzatoare
        public static ImageSource GetImg(Jucator culoare, TipPiesa tip)
        {

            return culoare switch
            {
                Jucator.Alb => SursaA[tip],
                Jucator.Negru => SursaN[tip],
                _ => null
            };
        }

        public static ImageSource GetImg(Piesa piesa)
        {

                if (piesa == null)
                {
                    return null;
                }

                return GetImg(piesa.Culoare, piesa.Tip);

        }
        
    }
}
