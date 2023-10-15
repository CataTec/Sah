using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaJoc
{
    public class Nebun : Piesa
    {

        //clase pentru suprascriea claselor abstracte care determina tipul si culoarea piesei
        public override TipPiesa Tip => TipPiesa.Nebun;
        public override Jucator Culoare { get; }

        // preia o matrice pentru toate directiile in diagonala
        private static readonly Directie[] dirs = new Directie[]
        {

            Directie.SusStanga,
            Directie.SusDreapta,
            Directie.JosStanga,
            Directie.JosDreapta

        };

        //constructor care determina culoarea piesei in functie de jucator
        public Nebun(Jucator culoare)
        {
            Culoare = culoare;
        }

        //Copie folosita pentru a determina culuarea piesei care a fost mutata
        public override Piesa Copy()
        {
            Nebun copie = new Nebun(Culoare);
            copie.Mutat = Mutat;
            return copie;
        }

        // preia miscarea posibile si creaza posibilitatea de mutare pe acele pozitii
        public override IEnumerable<Miscari> GetMiscare(Pozitie din, Tabla tabla)
        {

            return MiscarePozDirs(din, tabla, dirs).Select(catre => new MiscNormala(din, catre));

        }
    }
}
