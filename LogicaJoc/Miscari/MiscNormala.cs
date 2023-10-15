using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaJoc
{
    public class MiscNormala : Miscari
    {
        public override TipMiscare Tip => TipMiscare.Normal; //tipul de miscare normala

        public override Pozitie DinPoz { get; } //pozitia din care pleaca piesa
        public override Pozitie CatrePoz { get; } //pozitia in care ajunge piesa

        // construcor care preia pozitii cele doua pozitii si le stocheaza in proprietati
        public MiscNormala(Pozitie din, Pozitie catre)
        {
            DinPoz = din;
            CatrePoz = catre;
        }

        // metoda pentru executia miscarii
        public override void Executa(Tabla tabla)
        {

            Piesa piesa = tabla[DinPoz];
            tabla[CatrePoz] = piesa;
            tabla[DinPoz] = null;
            piesa.Mutat = true;

        }
    }
}
