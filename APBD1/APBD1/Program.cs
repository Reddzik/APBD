using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace APBD1
{
    public class Program
    {
        public static List<string> mailList = new List<string>();
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
        public static async Task findEmails(string url, List list)
        {
           int startLength = list.Length;
            var client = new HttpClient();
            var result = await client.GetAsync(url);
            list<string> mailsList= list;
            if(!result.IsSuccesStatusCode) return;

            string html = await result.Content.ReadAsStringAsync();
            var emailRegex = new Regex("[a-z]+[a-z0-9]*@[a-z.]+", RegexOptions.IgnorCase);
            var mateches = emailRegex.Matches(html);
            foreach(var match in matches){
                mailsList.add(match);
            }
            string pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            var urlRegex = new Regex(pattern, RegexOption.IgnoreCase);
            var matches = urlRegex.Matches(html);
            foreach(var match in matches){
                findEmails(match,mailList);
            }
            return mailList;

        }
    }
}
