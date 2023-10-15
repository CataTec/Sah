using LogicaJoc.Piese;

namespace LogicaJoc
{
    public class Tabla
    {
        // O matrice pentru stocarea pozitiilor pieselor
        private readonly Piesa[,] piese = new Piesa[8, 8];

        // Permitem accesul prin folosirea unui index ce returneaza un rand si o coloana
        public Piesa this[int rand, int col]
        {
            get { return piese[rand, col]; }
            set { piese[rand, col] = value; }
        }

        // Determina pozitia piesei apeland indexul pentru rand si coloana
        public Piesa this[Pozitie poz]
        {
            get { return this[poz.Rand, poz.Coloana]; }
            set { this[poz.Rand, poz.Coloana] = value; }
        }

        //Metoda care creaza o Tabla goala pe care adauga piesele
        public static Tabla Initial()
        {
            Tabla tabla = new Tabla();
            tabla.AddPiesaStart();
            return tabla;
        }

        //Adaugarea piesele pe pozitiile initiale
        private void AddPiesaStart()
        {
            this[0, 0] = new Tura(Jucator.Negru);
            this[0, 1] = new Cal(Jucator.Negru);
            this[0, 2] = new Nebun(Jucator.Negru);
            this[0, 3] = new Regina(Jucator.Negru);
            this[0, 4] = new Rege(Jucator.Negru);
            this[0, 5] = new Nebun(Jucator.Negru);
            this[0, 6] = new Cal(Jucator.Negru);
            this[0, 7] = new Tura(Jucator.Negru);


            this[7, 0] = new Tura(Jucator.Alb);
            this[7, 1] = new Cal(Jucator.Alb);
            this[7, 2] = new Nebun(Jucator.Alb);
            this[7, 3] = new Regina(Jucator.Alb);
            this[7, 4] = new Rege(Jucator.Alb);
            this[7, 5] = new Nebun(Jucator.Alb);
            this[7, 6] = new Cal(Jucator.Alb);
            this[7, 7] = new Tura(Jucator.Alb);

            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Pion(Jucator.Negru);
                this[6, i] = new Pion(Jucator.Alb);
            }
        }
        //Verifica daca pozitia este in intriorul tablei
        public static bool Interior(Pozitie poz)
        {
            return poz.Rand >= 0 && poz.Rand < 8 && poz.Coloana >= 0 && poz.Coloana < 8;
        }

        // Verifica daca pe pozitia respectiva se afla o piesa
        public  bool Gol(Pozitie poz)
        {
            return this[poz] == null;
        }

        // Verifica care sunt pozitiile ocupate de piese
        public IEnumerable<Pozitie> PozitiePiesa() 
        {

            for (int r = 0; r<8; r++)
            {
                for(int c = 0; c < 8; c++)
                {
                    Pozitie poz = new Pozitie(r, c);

                    if (!Gol(poz))
                    {
                        yield return poz;
                    }
                }
            }

        }

        // Verifica ce poziti sunt ocupate de piese de o anumita culoare
        public IEnumerable<Pozitie> PozitiePiesaPt(Jucator jucator)
        {
            return PozitiePiesa().Where(poz => this[poz].Culoare == jucator);
        }

        // Metoda care verifica daca Regele jucatorului curent este in Sah
        public bool EsteSah(Jucator jucator)
        {
            return PozitiePiesaPt(jucator.Oponent()).Any(poz =>
            {
                Piesa piesa = this[poz];
                return piesa.PoateCapturaRege(poz, this);
            });
        }

        // Face o copie a tablei cu pisele pe pozitile actuale
        public Tabla Copiaza()
        {
            Tabla copie = new Tabla();
            foreach (Pozitie poz in PozitiePiesa())
            {
                copie[poz] = this[poz].Copy();
            }

            return copie;
        }

    }
}
