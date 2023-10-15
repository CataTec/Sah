namespace LogicaJoc
{

    public class Pion : Piesa
    {
        
        //clase pentru suprascriea claselor abstracte care determina tipul si culoarea piesei
        public override TipPiesa Tip => TipPiesa.Pion;
        public override Jucator Culoare  { get; }

        // variabila de directie pentru miscarea inainte
        private readonly Directie inainte;

        //constructor care determina culoarea pionului in functie de jucator pentru a determina directia
        public Pion(Jucator culoare)
        
         {
             Culoare = culoare;

            if(culoare == Jucator.Alb)
            {
                inainte = Directie.Sus;
            } else if (culoare == Jucator.Negru)
            {
                inainte = Directie.Jos;
            }
         }

        //Copie folosita pentru a determina culuarea piesei care a fost mutata
       public override Piesa Copy()
        {
            Pion copie = new Pion(Culoare);
            copie.Mutat = Mutat;
            return copie;
        }

        // metoda care determina daca pionul poate sa se miste inainte
        private static bool PoateMisca(Pozitie poz, Tabla tabla)
        {
            return Tabla.Interior(poz) && tabla.Gol(poz);
        }

        //verifica daca o piesa de alta culoare se afla pe diagonala data de pion
        private bool PoateCaptura(Pozitie poz, Tabla tabla)
        {
            if (!Tabla.Interior(poz) || tabla.Gol(poz))
            {
                return false;
            }

            return tabla[poz].Culoare != Culoare;
        }

        //metoda pentru verificarea misacarilor, poate sa returneze si miscarea cu 2 pozitii a pionului
        private IEnumerable<Miscari> MiscareInainte(Pozitie din, Tabla tabla)
        {

            Pozitie OMiscare = din + inainte;

            if(PoateMisca(OMiscare, tabla))
            {
                yield return new MiscNormala(din, OMiscare);

                Pozitie DMiscare = OMiscare + inainte;

                if(!Mutat && PoateMisca(DMiscare, tabla))
                {
                    yield return new MiscNormala(din, DMiscare);
                }
            }

        }

        //metoda pentru verificarea misacarilor in diagonala
        private IEnumerable<Miscari> MiscareDiagonala(Pozitie din, Tabla tabla)
        {
            foreach (Directie dir in new Directie[] {Directie.Dreapta, Directie.Stanga })
            {
                Pozitie catre = din + inainte + dir;

                if (PoateCaptura(catre, tabla))
                {
                    yield return new MiscNormala(din, catre);
                }
            }

            
        }

        // retuneaza miscarile legale
        public override IEnumerable<Miscari> GetMiscare(Pozitie din, Tabla tabla)
        {
            return MiscareInainte(din, tabla).Concat(MiscareDiagonala(din, tabla));
        }

        // Metoda careverifica daca Pionul poate captura regele pe diagonala
        public override bool PoateCapturaRege(Pozitie din, Tabla tabla)
        {
            return MiscareDiagonala(din,tabla).Any(miscare =>
            {
                Piesa piesa = tabla[miscare.CatrePoz];
                return piesa != null && piesa.Tip == TipPiesa.Rege;
            });
        }

    }

}

