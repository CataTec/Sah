namespace LogicaJoc
{
    public class StadiuJoc
    {
        public Tabla Tabla { get; } // Preia clasa tabla
        public Jucator JucatorCurent { get; private set; } // Preia clasa jucator

        public Rezultat Rezultat { get; private set; } = null;  // Adauga o proprietate de tip Rezultat

        // Determina a carui jucator este randul
        public StadiuJoc(Jucator jucator, Tabla tabla)
        {
            JucatorCurent = jucator;
            Tabla = tabla;
        }

        // metoda pentru determinarea miscarilor legale si le elimina pe cele care pune Regele in sah 
        public IEnumerable<Miscari> MiscariLegale(Pozitie poz) 
        {
            if(Tabla.Gol(poz) || Tabla[poz].Culoare != JucatorCurent)
            {
                return Enumerable.Empty<Miscari>();
            }

            Piesa piesa = Tabla[poz];
            IEnumerable<Miscari> miscareCandidati = piesa.GetMiscare(poz, Tabla);
            return miscareCandidati.Where(miscare => miscare.EsteLegal(Tabla));
        }

        // metoda pentru executarea miscarii pe care o alege utilizatorul
        public void CreareMiscare(Miscari miscare)
        {
            miscare.Executa(Tabla);
            JucatorCurent = JucatorCurent.Oponent();
            VerificaSfarsit();
        }

        // metoda care verifica care verifica toate miscarile posibile ale jucatorului
        public IEnumerable<Miscari> ToateMiscariLegale(Jucator jucator)
        {
            IEnumerable<Miscari> miscareCandidati = Tabla.PozitiePiesaPt(jucator).SelectMany(poz =>
            {
                Piesa piesa = Tabla[poz];
                return piesa.GetMiscare(poz, Tabla);
            });

            return miscareCandidati.Where(miscare => miscare.EsteLegal(Tabla));
        }

        // metoda care verifica daca nu se mai pot face miscari si dac este Victorie sau Egal
        private void VerificaSfarsit()
        {
            if (!ToateMiscariLegale(JucatorCurent).Any())
            {
                if (Tabla.EsteSah(JucatorCurent))
                {
                    Rezultat = Rezultat.Victorie(JucatorCurent.Oponent());
                }
                else
                {
                    Rezultat = Rezultat.Egal(MotivSfarsit.Egal);
                }
            }
        }

        // Metoda care determina daca jocul sa terminat
        public bool GameOver()
        {
            return Rezultat != null;
        }
    }
}
