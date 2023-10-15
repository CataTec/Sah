namespace LogicaJoc
{
    public class Rezultat
    {
        public Jucator Casticgator { get; } // Preia jucatorul care a castigat
        public MotivSfarsit Motiv {  get; } // Preia motivul pentru sfarsitul jocului

        // Contructor care preia valorile si le stocheaza
        public Rezultat(Jucator casticgator, MotivSfarsit motiv)
        {
            Casticgator = casticgator;
            Motiv = motiv;
        }

        // Verifica daca jucatorul curent a castigat
        public static Rezultat Victorie(Jucator castigator)
        {
            return new Rezultat(castigator, MotivSfarsit.Sahmat);
        }

        // Verifica daca este egal
        public static Rezultat Egal(MotivSfarsit motiv)
        {
            return new Rezultat(Jucator.Gol, motiv);
        }

    }
}
