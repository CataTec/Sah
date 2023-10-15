namespace LogicaJoc.Piese
{
    public class Tura : Piesa
    {

        //clase pentru suprascriea claselor abstracte care determina tipul si culoarea piesei
        public override TipPiesa Tip => TipPiesa.Tura;
        public override Jucator Culoare { get; }

        // preia o matrice pentru toate directiile drepte
        private static readonly Directie[] dirs = new Directie[]
        {
            Directie.Sus,
            Directie.Jos,
            Directie.Dreapta,
            Directie.Stanga
        };

        //constructor care determina culoarea piesei in functie de jucator
        public Tura(Jucator culoare)
        {
            Culoare = culoare;
        }

        //Copie folosita pentru a determina culuarea piesei care a fost mutata
        public override Piesa Copy()
        {
            Tura copie = new Tura(Culoare);
            copie.Mutat = Mutat;
            return copie;
        }

        // preia miscarea posibile si creaza posibilitatea de mutare pe acele pozitii
        public override IEnumerable<Miscari> GetMiscare(Pozitie din, Tabla tabla)
        {

            return MiscarePozDirs(din, tabla, dirs).Select(catre => new MiscNormala(din, catre));

        }
    }
}
