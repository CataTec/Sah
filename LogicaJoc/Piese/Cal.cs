using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaJoc
{
    public class Cal : Piesa
    {

        //clase pentru suprascriea claselor abstracte care determina tipul si culoarea piesei
        public override TipPiesa Tip => TipPiesa.Cal;
        public override Jucator Culoare { get; }

        //constructor care determina culoarea piesei in functie de jucator
        public Cal(Jucator culoare)
        {
            Culoare = culoare;
        }

        //Copie folosita pentru a determina culuarea piesei care a fost mutata
        public override Piesa Copy()
        {
            Cal copie = new Cal(Culoare);
            copie.Mutat = Mutat;
            return copie;
        }

        //verifica directriile in care se misca calul
        private static IEnumerable<Pozitie> Salt(Pozitie din)
        {
            foreach(Directie vDir in new Directie[] {Directie.Sus, Directie.Jos}) 
            {
                foreach(Directie oDir in new Directie[] { Directie.Dreapta, Directie.Stanga })
                {
                    yield return din + 2 * vDir + oDir;
                    yield return din + 2 * oDir + vDir;
                }
            }
        }

        // metoda pentru determinarea pozitiilor in care calul se poate misca
        private  IEnumerable<Pozitie> MiscarePoz(Pozitie din,Tabla tabla)
        {
            return Salt(din).Where(poz => Tabla.Interior(poz) 
            && (tabla.Gol(poz) || tabla[poz].Culoare != Culoare));
        }

        // efectueaza miscarea
        public override IEnumerable<Miscari> GetMiscare(Pozitie din, Tabla tabla)
        {
            return MiscarePoz(din, tabla).Select(catre => new MiscNormala(din, catre));
        }
    }
}
