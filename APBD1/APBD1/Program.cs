using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace APBD1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string url = args.Length>0 ? args[0] : "https://www.pja.edu.pl/";

            var client = new HttpClient();
            var result = await client.GetAsync(url);
            //Task === promise
            //ThreadPool() pula wątków 
            if (!result.IsSuccessStatusCode)
                return;

            //kolekcje
            var zbiory = new HashSet<string>();
            var slownik = new Dictionary<string, int>();



            string html = await result.Content.ReadAsStringAsync();
            var regex = new Regex("[a-z]+[a-z0-9]*@[a-z.]+",
                RegexOptions.IgnoreCase);
            var matches = regex.Matches(html);
            foreach(var match in matches)
            {
                Console.WriteLine($"It's a match! {match}");
            }

        }
    }
}
