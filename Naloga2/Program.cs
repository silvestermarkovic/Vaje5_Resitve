using System;
using System.Threading;
using System.Threading.Tasks;

namespace Naloga2
{
    class Program
    {


        static void Main(string[] args)
        {
            Random _random = new Random();

            var b1 = Razporeditelj.VrniRazporeditelj();
            var b2 = Razporeditelj.VrniRazporeditelj();
            var b3 = Razporeditelj.VrniRazporeditelj();
            var b4 = Razporeditelj.VrniRazporeditelj();

            //Prikaz delovanja Singelton
            //Alarem sprožimo na enem, in je sprožen na vseh, gre za isto instanco
            b4.sprozenAlarm = true;
            Console.WriteLine($"b3 {b3.sprozenAlarm}");
            Console.WriteLine($"B2 {b2.sprozenAlarm}");
            b2.sprozenAlarm = false;
            Console.WriteLine($"B4 {b4.sprozenAlarm}");
            Console.WriteLine($"b2 {b2.sprozenAlarm}");

            // Potrditev, da gre za isto instance
            if (b1 == b2 && b2 == b3 && b3 == b4)
            {
                Console.WriteLine("Same instance\n");
            }


            // Razporejevalniku bomo ustvarili naloge, z različnim časom in obremenitvijo
            var razporediteljbremena = Razporeditelj.VrniRazporeditelj();
            /*
            for (int i = 0; i < 10; i++)
            {
                int teza = _random.Next(10, 30);

                Obremenitev(_random.Next(2, 6), razporediteljbremena.NaslednjiStreznik(teza), teza);
            }
            */
            for (int i = 0; i < 200; i++)
            {
                int teza = _random.Next(10, 30);
                //zaženemo Task in nadaljujemo
                Task<int> naloga = ObremenitevAsync(_random.Next(2, 6), razporediteljbremena.NaslednjiStreznik(teza), teza);

                //

            }
            Console.WriteLine("Razporejeno\n");
            // Počakamo pred koncem
            Console.ReadKey();

        }
        //
        //metoda nam bo obremenila strezik za dolocsen cas
        //metoda ja async (več je predvideno v nadaljevanju)
        public static async Task<int> ObremenitevAsync(double cas, Streznik pstreznik, int pteza)
        {
            //povečamo StKlicev
            pstreznik.StKlicev += 1;
            //dodamo obremenitev
            pstreznik.Obremenitev += pteza;

            //zapisemo si trenutno stevilo klicev (ker sicer bi pstreznik vrnil trenutno)
            int temp = pstreznik.StKlicev;

            Console.WriteLine("Zacetek (" + temp + "/" + pstreznik.StKlicev + "): " + pstreznik.Name + " Cas: " + cas + "s  Zahtevnost:" + pteza + " " + pstreznik.Obremenitev + "/" + pstreznik.ObremenitevMax);

            //podatki moramo v milisekundah zato * 1000            
            int sek = (int)cas * 1000;
            //await pove, da async počaka, da se izvrši ukaz, sicer bi nadaljevalo takoj
            await Task.Delay(sek);
            //odstranimo obremenitev
            pstreznik.Obremenitev -= pteza;
            Console.WriteLine("Konec (" + temp + "/" + pstreznik.StKlicev + "): " + pstreznik.Name + " Cas: " + cas + "s  Zahtevnost:" + pteza + " " + pstreznik.Obremenitev + "/" + pstreznik.ObremenitevMax);

            return 0;
        }
        public static void Obremenitev(double cas, Streznik pstreznik, int pteza)
        {
            //povečamo StKlicev
            pstreznik.StKlicev += 1;
            //dodamo obremenitev
            pstreznik.Obremenitev += pteza;

            //zapisemo si trenutno stevilo klicev (ker sicer bi pstreznik vrnil trenutno)
            int temp = pstreznik.StKlicev;

            Console.WriteLine("Zacetek (" + temp + "/" + pstreznik.StKlicev + "): " + pstreznik.Name + " Cas: " + cas + "s  Zahtevnost:" + pteza + " " + pstreznik.Obremenitev + "/" + pstreznik.ObremenitevMax);

            //podatki moramo v milisekundah zato * 1000            
            int sek = (int)cas * 1000;
            //await pove, da async počaka, da se izvrši ukaz, sicer bi nadaljevalo takoj
            Thread.Sleep(sek);
            //odstranimo obremenitev
            pstreznik.Obremenitev -= pteza;
            Console.WriteLine("Konec (" + temp + "/" + pstreznik.StKlicev + "): " + pstreznik.Name + " Cas: " + cas + "s  Zahtevnost:" + pteza + " " + pstreznik.Obremenitev + "/" + pstreznik.ObremenitevMax);

        }
    }
}
