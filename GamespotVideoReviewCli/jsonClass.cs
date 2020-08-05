using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamespotVideoReviewCli
{

    class CSVClass
    {
        public string Title { get; set; }
        public string GameName { get; set; }
        public string PublishDate { get; set; }
        public string Blurb { get; set; }
        public string HighVideo { get; set; }
        public string LowVideo { get; set; }
        public string HDVideo { get; set; }
        public string ReviewUrl { get; set; }
    }

    class jsonClass
    {
        public class Rootobject
        {
            public string error { get; set; }
            public int limit { get; set; }
            public int offset { get; set; }
            public int number_of_page_results { get; set; }
            public int number_of_total_results { get; set; }
            public int status_code { get; set; }
            public List<Result> results { get; set; }
            public string version { get; set; }
        }

        public class Result
        {
            public int length_seconds { get; set; }
            public string publish_date { get; set; }
            public string mpx_id { get; set; }
            public int id { get; set; }
            public string title { get; set; }
            public Image image { get; set; }
            public string deck { get; set; }
            public string source { get; set; }
            public Category[] categories { get; set; }
            public List<object> associations { get; set; }
            public string high_url { get; set; }
            public string low_url { get; set; }
            public string hd_url { get; set; }
            public string site_detail_url { get; set; }
        }

        public class Image
        {
            public string square_tiny { get; set; }
            public string screen_tiny { get; set; }
            public string square_small { get; set; }
            public string original { get; set; }
        }

        public class Category
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Association
        {
            public string name { get; set; }
        }
    }
}
