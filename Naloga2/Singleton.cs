using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Naloga2
{
    //2.1. Ustvarite razred Strezniki
    //razred ima naslednje propertije:
    //javno ime (get/set)           string
    //javno ip (get/set)            string
    //javno stKlicev (get/set)      int
    //javno obremenitev (get/set)   double  trenutna obremenitev
    //javno ObremenitevMax (get/set) double maksimalna obremenitev streznika
    public class Streznik
    {
        // Gets or sets server name
        public string Name { get; set; }
        // Gets or sets server IP address
        public string Ip { get; set; }
        public int StKlicev { get; set; }
        public double Obremenitev { get; set; }
        public double ObremenitevMax { get; set; }

        public bool VServisu { get; set; }
    }




    //ustvarite sealed class Razporeditelj
    public sealed class Razporeditelj

    {
        // Static members are 'eagerly initialized', that is, 
        // immediately when class is loaded for the first time.
        // .NET guarantees thread safety for static initialization

        private static readonly Razporeditelj _instance = new Razporeditelj();

        // Type-safe generic list of servers
        //2.2. ustvarite privatni seznam _strezniki tipa Strezniki (get/set)
        private List<Streznik> _strezniki { get; set; }

        public bool sprozenAlarm = false;


        // Pazite: konstruktor je 'private'
        //2.3. Naredite konstruktor, ki bo napolnil _strezniki s podatki o 3-5 streznikih
        //StKlicev naj bo vedno enako 0, ObremenitevMax naj bo med 100 in 200
        private Razporeditelj()
        {

          
            // Load list of available servers
            _strezniki = new List<Streznik>
                    {
                        new Streznik{ Name = "Server1", Ip = "10.10.10.1", StKlicev = 0, ObremenitevMax = 100, VServisu = false},
                        new Streznik{ Name = "Server2", Ip = "10.10.10.2", StKlicev = 0, ObremenitevMax = 100, VServisu = false},
                        new Streznik{ Name = "Server3", Ip = "10.10.10.3", StKlicev = 0, ObremenitevMax = 200, VServisu = false},
                };
        }


        //vrne _instance, ki ob incalizaciji kliče konstruktor, ki je private
        public static Razporeditelj VrniRazporeditelj()
        {
            return _instance;
        }

        // Simple, but effective load balancer

        public Streznik NaslednjiStreznik(int pteza)
        {

            //--------------------------------------------------------------------------------
            //Primer: naključno razporejanje
            //int random = _random.Next(_strezniki.Count);
            //preverimo, če je razpoložljiv

            //če nakjučni strežnik zmore izberemo tega
            /* if (_strezniki[random].ObremenitevMax > pteza + _strezniki[random].Obremenitev) {
               return _strezniki[random];
           }*/
            //sicer vrnemo z največ proste kapacitete
            //_strezniki[random].StKlicev += 1;
            //--------------------------------------------------------------------------------



            //2.4. spremenite tako, da bo obremenilo, najmanj obremenjeni strežnik
            while (true)
            {
                for (int i = 0; i < _strezniki.Count; i++)
                {
                    //TODO 2.1 z Uporaba Linq vrnite strežnik, ki je najmanj obremenjen procentualno in  zmore breme

                    var poizv5 = (from str in _strezniki
                                 where str.VServisu == false && str.ObremenitevMax >= (pteza + str.Obremenitev)
                                 orderby str.Obremenitev / str.ObremenitevMax
                                  select str).Take(1);
                    
                    if (poizv5.Count() > 0 )
                        return poizv5.First();

                    foreach (Streznik elt in poizv5)
                    {
                        Console.WriteLine($"{elt.Obremenitev/ elt.ObremenitevMax}");
                        return elt;
                    }
                    

                    if (_strezniki[i].VServisu == false && _strezniki[i].ObremenitevMax >= (pteza + _strezniki[i].Obremenitev))
                    {
                        return _strezniki[i];
                    }
                }
                 

                //če ni kapacitet počakamo 0.5s, če se sporstijo kapacitete
                Console.WriteLine("Vse kapacitete zasedene, čakamo!");
                Thread.Sleep(500);
            }
        }
    }


}


