namespace LogicaJoc
{

    //Clasa folosita pentru a determina directia in care se misca o piesa
    public class Directie
    {
        public readonly static Directie Sus = new Directie(-1, 0);
        public readonly static Directie Jos = new Directie(+1, 0);
        public readonly static Directie Stanga = new Directie(0, +1);
        public readonly static Directie Dreapta = new Directie(0, -1);
        public readonly static Directie SusStanga = Sus + Stanga;
        public readonly static Directie JosStanga = Jos + Stanga;
        public readonly static Directie SusDreapta = Sus + Dreapta;
        public readonly static Directie JosDreapta = Jos + Dreapta;
        public int DeltaRand { get; }
        public int DeltaColoana { get; }
        
        //Initializea valorile pentru rand si coloana 
        public Directie(int deltaRand, int deltaColoana) 
        {
            DeltaRand = deltaRand;
            DeltaColoana = deltaColoana;
        }
        
        // Clasa folosita pentru mersul pe diagonala combinand doua directii
        public static Directie operator +(Directie dir1, Directie dir2) 
        {
            return new Directie(dir1.DeltaRand + dir2.DeltaRand, dir1.DeltaColoana + dir2.DeltaColoana);
        }

        public static Directie operator *(int scalar, Directie dir)
        {
            return new Directie(scalar * dir.DeltaRand, scalar * dir.DeltaColoana);
        }
    }
}
