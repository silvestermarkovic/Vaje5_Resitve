
using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;

namespace Naloga3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Zaposleni> seznam = new List<Zaposleni>();

            string vsebina = Vrnivsebino("http://dummy.restapiexample.com/api/v1/employees");

            //vsebina povezave
            Console.WriteLine($"{vsebina}");

            //TODO 3.1
            //dodajte Nuget Newtonsoft.Json in odkomentirajte 
            
            JToken token = JToken.Parse(vsebina);
            JArray zaposleni = (JArray)token.SelectToken("data");
            foreach (JToken zap in zaposleni)
            {
                seznam.Add(new Zaposleni() { id = (int)zap["id"], employee_name = (string)zap["employee_name"], employee_age = (int)zap["employee_age"], employee_salary = (double)zap["employee_salary"] });
            }
            //*/



            //TODO 3.2
            //izracunajte povprecno placo in jo shranite v spremenljivko: povprecnaplaca
            double povprecnaplaca = seznam.Average(s => s.employee_salary);


            //po vsaki poizvedbi izpisite seznam (naredi rezsiritev)
            //TODO 3.3
            //ustvarite seznam ljudi, ki majo placo visjo od povprecneplace z uporabo Linq
            var poiz1 = from zap1 in seznam
                        where zap1.employee_salary > povprecnaplaca
                        select zap1;
            poiz1.ReadEnumerable();

            //TODO 3.4
            //izpisite zaposlene, ki so stari med 30 in 50 let, razvrstite jih po placi padajoce
            var poiz2 = from zap2 in seznam
                        where zap2.employee_age  >= 30  && zap2.employee_age <= 50
                        select zap2;
            poiz2.ReadEnumerable();


            //TODO 3.5
            //grupirajte zaposlene glede na starost (3x) je grupa 3, (4x)je grupa 4 in inzračunajte seštevek njihovih, plač in koliko je zaposlenih v tej grupi
            var poiz3 = from zap2 in seznam
                        group zap2 by (int)zap2.employee_age / 10 into gr
                        select new
                        {
                            gr.Key,
                            Stevilo = gr.Count(),
                            SestevkPlac = gr.Sum(s=> s.employee_salary)
                        };
            poiz3.ReadEnumerable();

        }

        static string Vrnivsebino(string url)
        {
            string vsebina = "";
            using (var webClient = new System.Net.WebClient())
            {
                vsebina = webClient.DownloadString(url);
            }
            return vsebina;
        }
    }

    public static class Extensions
    {
        public static void ReadEnumerable<T>(this IEnumerable<T> list)
        {
            Console.Write("Elementi seznama so: ");
            int count = 0;
            foreach (var item in list)
            {
                count++;
                Console.WriteLine(item.ToString() + $"{(count == list.Count() ? Environment.NewLine : ",")} ");
            }
            Console.WriteLine();
        }
    }

}
