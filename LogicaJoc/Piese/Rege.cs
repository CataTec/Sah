using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaJoc.Piese
{
    public class Rege : Piesa
    {

        //clase pentru suprascriea claselor abstracte care determina tipul si culoarea piesei
        public override TipPiesa Tip => TipPiesa.Rege;
        public override Jucator Culoare { get; }

        // determina directiile de miscare
        private static readonly Directie[] dirs = new Directie[]
        {


            Directie.Sus,
            Directie.Jos,
            Directie.Dreapta,
            Directie.Stanga,
            Directie.SusStanga,
            Directie.SusDreapta,
            Directie.JosStanga,
            Directie.JosDreapta


        };

        //constructor care determina culoarea piesei in functie de jucator
        public Rege(Jucator culoare)
        {
            Culoare = culoare;
        }

        //Copie folosita pentru a determina culuarea piesei care a fost mutata
        public override Piesa Copy()
        {
            Rege copie = new Rege(Culoare);
            copie.Mutat = Mutat;
            return copie;
        }

        //verifica in ce directie se poate misca cu un pas
        private IEnumerable<Pozitie> SchimbPoz(Pozitie din, Tabla tabla)
        {

            foreach (Directie dir in dirs)
            {
                Pozitie catre = din + dir;

                if(!Tabla.Interior(catre))
                {
                    continue;
                }

                if (tabla.Gol(catre) || tabla[catre].Culoare != Culoare)
                {
                    yield return catre;
                }
            }

        }

        //efectueaza miscarea
        public override IEnumerable<Miscari> GetMiscare(Pozitie din, Tabla tabla)
        {

            foreach (Pozitie catre in SchimbPoz(din, tabla))
            {
                yield return new MiscNormala(din, catre);
            }

        }

        // verifica daca Regele oponentului poate fi capturat
        public override bool PoateCapturaRege(Pozitie din, Tabla tabla)
        {
            return SchimbPoz(din, tabla).Any(catre =>
            {
                Piesa piesa = tabla[catre];
                return piesa != null && piesa.Tip == TipPiesa.Rege;
            });
        }
    }
}
