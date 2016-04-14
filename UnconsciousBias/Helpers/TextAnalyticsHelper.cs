using System;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
//using System.Web;

namespace UnconsciousBias.Helpers
{
    class TextAnalyticsHelper
    {
        /// <summary>
        /// Azure portal URL.
        /// </summary>
        private const string BaseUrl = "https://westus.api.cognitive.microsoft.com/";

        /// <summary>
        /// Your account key goes here.  Request access to the TextAnalytics service from https://www.microsoft.com/cognitive.  
        /// </summary>
        private const string AccountKey = "TODO";

        public static async Task<List<double>> GetSentiment(string textToProcess)
        {
            List<double> sentimentScores = new List<double>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                // Request headers.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AccountKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                byte[] byteData = Encoding.UTF8.GetBytes(textToProcess);

                // Detect sentiment:
                var uri = "text/analytics/v2.0/sentiment";
                string response = await CallEndpoint(client, uri, byteData);
                Debug.WriteLine("Sentiment response:" + response.ToString());

                var jResult = JObject.Parse(response);
                foreach (JObject doc in jResult["documents"])
                {
                    string score = (string)doc["score"];
                    double dblScore = Convert.ToDouble(score);
                    sentimentScores.Add(dblScore);
                }

                return sentimentScores;
            }
        }


        private static async Task<String> CallEndpoint(HttpClient client, string uri, byte[] byteData)
        {
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(uri, content);
                return await response.Content.ReadAsStringAsync();
            }
        }


    }
}
