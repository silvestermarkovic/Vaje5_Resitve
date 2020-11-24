using System;
using System.Collections.Generic;
using System.Linq;

namespace Naloga1
{
    class Program
    {
        static void Main(string[] args)
        {



            List<Kupec> kupci = new List<Kupec>();
            kreirajSeznamKupci(kupci);
            List<Dokument> dokumenti = new List<Dokument>();
            kreirajSeznamDokument(dokumenti);

            //TODO 1.1 ustvarite Linq poizvedbo, ki bo vrnila vse kupce iz Avstije ali Nemčije, vrnite samo naziv kup.naziv
            // var poizv1 = ....

            var poizv1 = from kup0 in kupci
                                  where kup0.drzava == "Avstrija" || kup0.drzava == "Nemčija"
                                  select new { kup0.naziv };
            //izpišite uporabite Extension  ReadEnumerable
            poizv1.ReadEnumerable();
            //nad18Linnad18Linq.ToList().ForEach(x => Console.WriteLine($"   {x.ID} {x.naziv}  {x.Starost} {x.DodatnoPolje}  "));
            // q.ToList().ForEach(x => Console.WriteLine($"   {x.ID} {x.naziv}  {x.Starost} {x.DodatnoPolje}  "));
            
            /*
            
            var nad18met = sez1.Where(s => s.starost > limitstarost).OrderBy(s => (double)s.starost / 120).
                Select(s => new { s.ID, s.naziv, Starost = (s.starost), DodatnoPolje = ((double)s.starost / 120) }).Take(1);
            nad18met.ToList().ForEach(x => Console.WriteLine($"   {x.ID} {x.naziv}  {x.Starost} {x.DodatnoPolje}  "));
            sez1.Add(new Kupec(4, "naziv4", 19));
            nad18met.ToList().ForEach(x => Console.WriteLine($"   {x.ID} {x.naziv}  {x.Starost} {x.DodatnoPolje}  "));

            */

            //ustvarite poizvedbo v method obliki
            //  var poizv1M = kupci.Select(s).Where(s. drzava => drzava == "")
            var poizvM = kupci.Where(s => s.drzava == "Avstija" || s.drzava == "Nemčija").Select(s => s.naziv);


            //TODO 1.2. dodajte kupca 21,Kupec21,Celovec,Avstrija
            //še 1x naredite poizvedbo (z izpisom), kaj opazite?
            dodajNaSeznamKupca(new Kupec(21, "Kupec21", "Celovec", "Avstrija"), kupci);
            poizv1.ReadEnumerable();


            //TODO 1.3. ustvarite seznam, ki bo vrnil kupce, ki NISO iz Slovenije, 
            //razvstite jih po državi, kraju in potem po nazivu, vrnite elemente od 6 do 10 mesta
            var poizv3 = (from kup3 in kupci
                                   where kup3.drzava != "Slovenija"
                                   orderby kup3.naziv
                                   orderby kup3.kraj
                                   orderby kup3.drzava
                                   select new { kup3.naziv, kup3.drzava, kup3.kraj, Dolz  = kup3.kraj.Length }).Take(10).Skip(5);
            var poizv3M = kupci.Where(s => s.drzava != "Slovenija").OrderBy(s => s.kraj).Take(10).Skip(5).Select(s => new { s.naziv,s.kraj  } ); ;
            poizv3.ReadEnumerable();
            poizv3M.ReadEnumerable();

            //TODO 1.4: ustvarite poizvedbo, ki bo vrnila, kupce razporejene po dolzini naziva kraja (naracsajoce) in nazivu
            //izpisejo naj se polja naziv, drzava,kraj in dolzina kraja (length) 
            var poizv4 = from kup2 in kupci
                                 orderby kup2.naziv
                                 orderby kup2.kraj.Length
                                 select new { kup2.naziv, kup2.drzava, kup2.kraj, Dolz = kup2.kraj.Length };

            poizv4.ReadEnumerable();
            var poizv4M = kupci.OrderBy(s => new { s.naziv, s.kraj.Length }).Select(s => new { s.naziv, s.drzava, s.kraj, Dolz = s.kraj.Length });


            //TODO 1.5.: prikazite vse dokumenta kupcev, ki imajo ID_kupca med 8 in 10
            //razvrstite po znesku naracajoce
            var poizv5 = from kup1 in kupci
                         join dok1 in dokumenti
                         on kup1.ID_kupca equals dok1.ID_kupca
                         where kup1.ID_kupca >= 8 && kup1.ID_kupca <= 10
                         orderby dok1.znesek
                         select new { dok1, kup1 };
                        //             select new { kup.naziv, kup.drzava, kup.kraj, Dolz = kup.kraj.Length };

            poizv5.ReadEnumerable();
          


            //TODO 1.6. naredite poizvedvo, ki bo vrnila seštevek po kupcih
            //izpiše naj ID_kupca, StRacunov (count), ZnesekRacunov
            var poizv6 = from dok5 in dokumenti
                         group dok5 by dok5.ID_kupca into gr
                         select new
                         {
                             NasKupec = gr.Key,
                             StRacunov = gr.Count(),
                             Znesek = gr.Sum(x => x.znesek)
                         };
                             
            poizv6.ReadEnumerable();


            //TODO 1.7: navodila sledijo
            var poizv7 = from kup7 in kupci
                         join dok7 in dokumenti
                         on kup7.ID_kupca equals dok7.ID_kupca
                         group dok7 by kup7.drzava into gr
                         orderby   gr.Sum(x => x.znesek)
                         select new
                         {
                             GrDrzava = gr.Key,
                             DrzavSt = gr.Count(),
                             ZnesekDrzava = gr.Sum(x => x.znesek),
                             PovprecniRacunDrzava= gr.Average(x => x.znesek)
                         };

            poizv7.ReadEnumerable();
        }




        public static void kreirajSeznamKupci( List<Kupec> pseznam)
        {


            dodajNaSeznamKupca(new Kupec(1,  "Kupec1", "Novo mesto", "Slovenija"), pseznam);
            dodajNaSeznamKupca(new Kupec(2, "Kupec2", "Slovenj Gradec", "Slovenija"), pseznam);
            dodajNaSeznamKupca(new Kupec(3, "Kupec3", "Celje", "Slovenija"), pseznam);
            dodajNaSeznamKupca(new Kupec(4, "Kupec4", "Maribor", "Slovenija"), pseznam);
            dodajNaSeznamKupca(new Kupec(5, "Kupec5", "Gradec", "Avstrija"), pseznam);
            dodajNaSeznamKupca(new Kupec(6, "Kupec6", "Dunaj", "Avstrija"), pseznam);
            dodajNaSeznamKupca(new Kupec(7, "Kupec7", "Linz", "Avstrija"), pseznam);
            dodajNaSeznamKupca(new Kupec(8, "Kupec8", "Trst", "Italija"), pseznam);
            dodajNaSeznamKupca(new Kupec(9, "Kupec9", "Rim", "Italija"), pseznam);
            dodajNaSeznamKupca(new Kupec(10, "Kupec10", "Torino", "Italija"), pseznam);
            dodajNaSeznamKupca(new Kupec(11, "Kupec11", "Milano", "Italija"), pseznam);
            dodajNaSeznamKupca(new Kupec(12, "Kupec12", "Pariz", "Francija"), pseznam);
            dodajNaSeznamKupca(new Kupec(13, "Kupec13", "London", "Velika Britanija"), pseznam);
            dodajNaSeznamKupca(new Kupec(14, "Kupec14", "Liverpool", "Velika Britanija"), pseznam);
            dodajNaSeznamKupca(new Kupec(15, "Kupec15", "Bonn", "Nemčija"), pseznam);
            dodajNaSeznamKupca(new Kupec(16, "Kupec16", "Berlin", "Nemčija"), pseznam);
            dodajNaSeznamKupca(new Kupec(17, "Kupec17", "Zagreb", "Hrvaška"), pseznam);
            dodajNaSeznamKupca(new Kupec(18, "Kupec18", "Zadar", "Hrvaška"), pseznam);
            dodajNaSeznamKupca(new Kupec(19, "Kupec19", "Madrid", "Španija"), pseznam);
            dodajNaSeznamKupca(new Kupec(20, "Kupec20", "Barcelona", "Španija"), pseznam);

        }

        public static void dodajNaSeznamKupca(Kupec pkupec, List<Kupec> pseznam)
        {
            pseznam.Add(pkupec);
        }


        public static void kreirajSeznamDokument(List<Dokument> pseznam)
        {

             
            var _rnd = new Random();

            for (int i = 0; i < 1000; i++)
            {
                dodajNaSeznamDokument(new Dokument("Racun" + i, _rnd.Next (1,20), _rnd.Next(100, 1000)), pseznam);
            }




        }
        public static void dodajNaSeznamDokument(Dokument pdokuemnt, List<Dokument> pseznam)
        {
            pseznam.Add(pdokuemnt);
        }
    }



   
}
