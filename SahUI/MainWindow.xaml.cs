using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using LogicaJoc;


namespace SahUI
{

    public partial class MainWindow : Window
    { 
        private readonly Image[,] ImgPiesa = new Image[8, 8];  // matrice pentru controlul imaginilor pieselor    
        private readonly Rectangle[,] highlights = new Rectangle[8, 8]; // pentru reprezentare luminii pozitiilor posibile
        private readonly Dictionary<Pozitie, Miscari> moveChache = new Dictionary<Pozitie, Miscari>();// dictionar pentru pozitie si miscari

        private StadiuJoc stadiuJ;
        private Pozitie pozSelect = null; //inital lumina nu apre pana cand o piesa este selectata

        
        public MainWindow() //Comportamentul jocului
        {
            InitializeComponent();
            StartHarta();

            stadiuJ = new StadiuJoc(Jucator.Alb, Tabla.Initial());
            DesenHarta(stadiuJ.Tabla);
            SetCursor(stadiuJ.JucatorCurent);
        }

        //Metoda folosita pentru a initia Harta(Tabla de Sah) impreuna cu piesele de pe aceasta
        private void StartHarta()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Image img = new Image(); //Pisele de pe tabla
                    ImgPiesa[r, c] = img;
                    PieceGrid.Children.Add(img);

                    Rectangle lumineaza = new Rectangle();// Lumina prentu pozitiile care urmeaza sa le ia
                    highlights[r,c] = lumineaza;
                    HighlightGrid.Children.Add(lumineaza);
                }
            }
        }

        private void DesenHarta(Tabla tabla) //preaia Tabla ca parametru si seteaza controlul pieselor pentru imagini
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piesa piesa = tabla[r, c];
                    ImgPiesa[r, c].Source = Imagini.GetImg(piesa);
                }

            }

        }

        // Metoda apelata cand mausul apasa pe o anumita pozitie
        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(BoardGrid);
            Pozitie poz = CatrePatrat(point);

            if (pozSelect == null)
            {
                DelaPozSelect(poz);
            }
            else
            {
                CatrePozSelect(poz);
            }
        }

        // Dimensiunea patratului selectat, folosit pentru a detecta pozitia
        private Pozitie CatrePatrat(Point point)
        {
            double patrat = BoardGrid.ActualWidth / 8;
            int rand = (int)(point.Y / patrat);
            int col = (int)(point.X / patrat);
            return new Pozitie(rand, col);
        }

        // Pozitia de unde este selectata piesa
        private void DelaPozSelect(Pozitie poz)
        {
            IEnumerable<Miscari> miscari = stadiuJ.MiscariLegale(poz);

            if(miscari.Any())
            {
                pozSelect = poz;
                CacheMoves(miscari);
                ArataLumina();
            }
        }

        // Pozitia selectata de jucator dupa selectia piesei
        private void CatrePozSelect(Pozitie poz)
        {
            pozSelect = null;
            AscundeLumina();

            if (moveChache.TryGetValue(poz, out Miscari miscare))
            {
                ManevreazaMiscari(miscare);
            }
        }

        // Metoda pentru efectuarea miscarii dupa ce au fost selectate
        private void ManevreazaMiscari(Miscari miscare)
        {
            stadiuJ.CreareMiscare(miscare);
            DesenHarta(stadiuJ.Tabla);
            SetCursor(stadiuJ.JucatorCurent);
        }

        // Clasa stocheaza colectia de miscari posibile, eliminand miscarile stocate dinainte
        private void CacheMoves(IEnumerable<Miscari> miscari)
        {
            moveChache.Clear();

            foreach (Miscari miscare in miscari)
            {
                moveChache[miscare.CatrePoz] = miscare;
            }

        }

        // Coloreaza (arata) patratele pe care poate fi mutata piesa
        private void ArataLumina()
        {
            Color culoare = Color.FromArgb(150, 125, 255, 125);

            foreach (Pozitie catre in moveChache.Keys)
            {
                highlights[catre.Rand, catre.Coloana].Fill = new SolidColorBrush(culoare);  
            }

        }

        // Ascnde culorile dupa ce au fost afisate
        private void AscundeLumina()
        {
            foreach (Pozitie catre in moveChache.Keys)
            {
                highlights[catre.Rand, catre.Coloana].Fill = Brushes.Transparent;
            }
        }

        // Metoda pentru setarea cursorului in functie de jucatorul curent
        private void SetCursor(Jucator jucator)
        {
            if(jucator == Jucator.Alb)
            {
                Cursor = CursorSah.CursorA;
            }
            else
            {
                Cursor = CursorSah.CursorN;
            }
        }
    }
}
