namespace LogicaJoc
{
    public abstract class Miscari
    {
        public abstract TipMiscare Tip { get; } //preia tipul de miscare

        public abstract Pozitie DinPoz { get; } //preia pozitia din care a fost mutata piesa

        public abstract Pozitie CatrePoz { get; } //preia pozitia in care a fost mutata piesa

        public abstract void Executa(Tabla tabla); //pentru executarea miscarii

        public virtual bool EsteLegal(Tabla tabla) //verifica daca este posibila miscarea in copie
        {
            Jucator jucator = tabla[DinPoz].Culoare;
            Tabla copieTabla = tabla.Copiaza();
            Executa(copieTabla);
            return !copieTabla.EsteSah(jucator);
        }
    }
}
