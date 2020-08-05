using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GamespotVideoReviewCli
{
    class Program
    {
        static void Main(string[] args)
        {
            int currentResults = 0;
            jsonClass.Rootobject gamespotJson = getJson(currentResults, 1);
            Console.WriteLine(gamespotJson.number_of_total_results + " video reviews found.");
            List<CSVClass> reviewList = new List<CSVClass>();

            while (currentResults <= gamespotJson.number_of_total_results)
            {
                gamespotJson = getJson(currentResults, 100);

                foreach (var item in gamespotJson.results)
                {
                    Console.WriteLine();
                    Console.WriteLine("###########################################################################");
                    Console.WriteLine();

                    CSVClass itemObj = new CSVClass();

                    itemObj.Title = item.title;
                    Console.WriteLine(item.title);

                    foreach (var ass in item.associations)
                    {
                        JObject jass = ass as JObject;

                        if (jass != null)
                        {
                            jsonClass.Association assresult = jass.ToObject<jsonClass.Association>();
                            itemObj.GameName = assresult.name;
                            Console.WriteLine(itemObj.GameName);
                        }
                    }


                    itemObj.PublishDate = item.publish_date;
                    Console.WriteLine(item.publish_date);

                    itemObj.Blurb = item.deck.Replace("\"","'").Replace("\n", "").Replace("\r", "");
                    Console.WriteLine(item.deck);

                    itemObj.HighVideo = item.high_url;
                    Console.WriteLine(item.high_url);

                    itemObj.LowVideo = item.low_url;
                    Console.WriteLine(item.low_url);

                    itemObj.HDVideo = item.hd_url;
                    Console.WriteLine(item.hd_url);

                    itemObj.ReviewUrl = item.site_detail_url;
                    Console.WriteLine(item.site_detail_url);

                    reviewList.Add(itemObj);
                }

                WriteCSV(reviewList, @"VideoReviews.csv");
                reviewList.Clear();
                currentResults = currentResults + 100;
            }
        }

        static jsonClass.Rootobject getJson(int offset, int limit)
        {
            string apiKey = "YOU GAMESPOT API KEY HERE";
            using (var client = new WebClient())
            {
                var settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;
                string json = client.DownloadString("https://www.gamespot.com/api/videos/?api_key=" + apiKey + "&format=json&filter=categories:122&offset=" + offset + "&limit=" + limit);
                return JsonConvert.DeserializeObject<jsonClass.Rootobject>(json, settings);
            }
        }

        static void WriteCSV<T>(IEnumerable<T> items, string path)
        {
            Type itemType = typeof(T);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            bool writeHeader = false;

            if (!File.Exists(path))
            {
                writeHeader = true;
            }

            using (var writer = new StreamWriter(path, true))
            {
                if (writeHeader == true)
                {
                    writer.WriteLine(string.Join("; ", props.Select(p => p.Name)));
                }

                foreach (var item in items)
                {
                    writer.WriteLine(string.Join("; ", props.Select(p => p.GetValue(item, null))));
                }
            }
        }
    }
}
