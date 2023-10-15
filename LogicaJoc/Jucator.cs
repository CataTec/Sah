namespace LogicaJoc
{

    // O clasa enum care este folosita pentru stocarea jucatorul a carui rand este, si pentru reprezentarea culorilor pieselor
    public enum Jucator
    {
        Gol,
        Alb,
        Negru
    }

    // O clasa in care este declarata o metoda folosita pentru a schimbat tura jucatorilor
    public static class ExtensiiJucator
    {
        public static Jucator Oponent(this Jucator jucator)
        {
            switch (jucator)
            {
                case Jucator.Alb:
                    return Jucator.Negru;
                case Jucator.Negru:
                    return Jucator.Alb;
                default: 
                    return Jucator.Gol;
            }
        }
    }
}
