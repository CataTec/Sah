namespace LogicaJoc
{ 
    public class Pozitie
    {
        // Clasa folosita pentru a determina dimensiunile pieselor
        public int Rand { get; }
        public int Coloana { get; }
        // Constructor pentru rand si coloana folosit pentru determinarea pozitiilor pieselor 
        public Pozitie(int rand, int coloana)
        {
            Rand = rand;
            Coloana = coloana;
        }

        // Determina culoarea patratului pe care se poate afla piesa
        public Jucator CuloarePatrat()
        {
            if ((Rand + Coloana) % 2 == 0)
            {
                return Jucator.Alb;
            }
            else
            {
                return Jucator.Negru;
            }
        }

        //clase folosite pentru ca clasa Pozitie sa fie folosita ca si cheie intrun dictionar 
        public override bool Equals(object obj)
        {
            return obj is Pozitie pozitie &&
                   Rand == pozitie.Rand &&
                   Coloana == pozitie.Coloana;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Rand, Coloana);
        }

        public static bool operator ==(Pozitie stanga, Pozitie dreapta)
        {
            return EqualityComparer<Pozitie>.Default.Equals(stanga, dreapta);
        }

        public static bool operator !=(Pozitie stanga, Pozitie dreapta)
        {
            return !(stanga == dreapta);
        }
        //Clasa ia o pozitie si o directie ca parametri si returneaza o noua pozitie prin efecturea unui singur pas catre pozitia dorita
        public static Pozitie operator +(Pozitie poz,Directie dir)
        {
            return new Pozitie(poz.Rand + dir.DeltaRand, poz.Coloana + dir.DeltaColoana);
        }

    }
}
