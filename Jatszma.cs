using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSakk
{
    public class Lepes
    {
        string _lepes;
        public char Tiszt => _lepes[0];
    }

    //todo Állapottér reprezentáció kialakítása
    public class Tabla
    {
        const char URES_MEZO = ' ';
        char[,] _tabla = new char[8, 8];

        public Tabla()
        {
            for (int i = 0; i < _tabla.GetLength(0); i++)
            {
                for (int j = 0; j < _tabla.GetLength(1); j++)
                {
                    _tabla[i, j] = URES_MEZO;
                }
            }
        }

        public void NyitoAllapotLetrehozasa()
        {
            //TODO bábuk felhelyzése
        }

        public void Lep(String ujLepes)
        {
            //hibák vizsgálata
            //throw new Exception();


            //todo léptesse oda a bábut, ahová kell
        }

        public char[,] tabla => _tabla;

        //TODO számon analitika, hasznos lekérdezések, stb.
    }

    public class Jatszma
    {
        List<String> lepesek;
        //todo Állapottér reprezentáció kialakítása V2.0

        /// <summary>
        /// Üres játék létrehozása
        /// </summary>
        public Jatszma()
        {
            lepesek = new List<String>();
        }
        public Jatszma(String fajlSor)
        {
            lepesek = new List<String>();
            foreach (var item in fajlSor.Trim().Split('\t'))
            {
                lepesek.Add(item);
            }
        }

        public int LepesekSzama => lepesek.Count();

        public char Nyertes => LepesekSzama % 2 == 0 ? 's' : 'v';

        //public int HuszarokLepesszama => lepesek.Count(lepes => lepes[0] == 'H');
        public int HuszarokLepesszama => TisztLepesszama('H');

        public int TisztLepesszama(char tisztJele)
        {
            return lepesek.Count(lepes => lepes[0] == tisztJele);
        }


        public List<String> FeherLepesek => lepesek.Where((lepes, index) => index % 2 == 0).ToList();
        public List<String> FeketeLepesek => lepesek.Where((lepes, index) => index % 2 == 1).ToList();

        public bool EgyikVezertUtottek(bool feher)
        {
            List<string> vezerLepesek = (feher ? FeherLepesek : FeketeLepesek).Where(x => x[0] == 'V').ToList();

            string utolsoPozicio;
            int utolsoIndex;
            if (vezerLepesek.Any())
            {
                utolsoPozicio = vezerLepesek.Last().Substring(vezerLepesek.Last().Length - 2);
                utolsoIndex = lepesek.IndexOf(vezerLepesek.Last());
            }
            else
            {
                utolsoPozicio = feher ? "d1" : "d8";
                utolsoIndex = 0;
            }

            return lepesek.Skip(utolsoIndex + 1).Any(x => x.Contains($"x{utolsoPozicio}"));
        }

        /// <summary>
        /// todo: Keresse meg mindkét vezér (királynő) utolsó pozícióját és nézze meg, hogy ott ütötték-e ezt a pozíviót? (vmi x poz)
        /// </summary>
        public bool VezertUttotek => EgyikVezertUtottek(true) || EgyikVezertUtottek(false);

        public int EgyikVezerLepett(bool feher) {
            List<string> vezerLepesek = (feher ? FeherLepesek : FeketeLepesek).Where(x => x[0] == 'V').ToList();
            string utolsoPozicio = feher ? "d1" : "d8";

            int mezok = 0;
            foreach (string lepes in vezerLepesek)
            {
                string pozicio = lepes.Substring(lepes.Length - 2);
                if (pozicio[1] != utolsoPozicio[1])
                {
                    mezok += Math.Abs(Convert.ToInt32(pozicio[1]) - Convert.ToInt32(utolsoPozicio[1]));
                }
                else
                {
                    mezok += Math.Abs(Convert.ToChar(pozicio[0]) - Convert.ToChar(utolsoPozicio[0]));
                }
                utolsoPozicio = pozicio;
            }

            return mezok;
        }

        public int VezerLepett => EgyikVezerLepett(true) + EgyikVezerLepett(false);

        public bool VilagosKiralyMozgott => FeherLepesek.Any(x => x[0] == 'K' || x[0] == 'O');

        public int MegmaradtBabuk => 32 - lepesek.Count(x => x.Contains('x'));
    }
}
