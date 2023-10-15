namespace LogicaJoc
{
    public abstract class Piesa
    {
        //Determina tipul piesei si culuarea, si daca piesa respectiva a fost mutata
        public abstract TipPiesa Tip { get; }
        public abstract Jucator Culoare { get; }
        public bool Mutat { get; set; } = false;

        //Copie piesa pentru a predetermina miscari legale sau posibile
        public abstract Piesa Copy();

        //metoda pentru a prelua miscarile folosind parametri pozitie si tabla, necesar pentru ca piesele nu isistoheaza poazitia me tabla
        public abstract IEnumerable<Miscari> GetMiscare(Pozitie din, Tabla tabla);

        //metoda care returneaza toate pozitiile posibile intr-o anumita directie
        protected IEnumerable<Pozitie> MiscarePozDir(Pozitie din, Tabla tabla, Directie dir)
        {
            // verifica toate pozitiile patrat cu patrat pana ajunge la sfarsitul tablei, sau intalneste alta piesa
            for (Pozitie poz = din + dir; Tabla.Interior(poz); poz += dir)
            {

                if (tabla.Gol(poz))
                {

                    yield return poz;
                    continue;

                }

                Piesa piesa = tabla[poz];

                if (piesa.Culoare != Culoare)
                {

                    yield return poz;

                }

                yield break;

            }

        }

        //preia o matrice de directii, este utilapentru toate directiile poeibile
        protected IEnumerable<Pozitie> MiscarePozDirs(Pozitie din, Tabla tabla, Directie[] dirs)
        {

            return dirs.SelectMany(dir => MiscarePozDir(din, tabla, dir));

        }

        // Metoda care verifica daca o piesa este intr-o pozitie in care poate captura regele
        public virtual bool PoateCapturaRege(Pozitie din,Tabla tabla)
        {
            return GetMiscare(din, tabla).Any(miscare =>
            {
                Piesa piesa = tabla[miscare.CatrePoz];
                return piesa != null && piesa.Tip == TipPiesa.Rege;
            });
        }
    }
}
