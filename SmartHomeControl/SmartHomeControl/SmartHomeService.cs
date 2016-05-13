using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeControl
{
    class SmartHomeService
    {
        private static SmartHomeService instance;

        public static SmartHomeService Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new SmartHomeService();
                }
                return instance;
            }
        }

        public async Task<string> FetchItemState(string itemName)
        {
            string url = $"http://192.168.1.205:8080/rest/items/{itemName}/state";

            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "text/plain";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    return reader.ReadToEnd();
                }
            }
        }

        public async Task PutItemState(string itemName, string state)
        {
            string url = $"http://192.168.1.205:8080/rest/items/{itemName}";

            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "text/plain";
            request.Method = "POST";

            var bytes = Encoding.UTF8.GetBytes(state);
            using (var requestStream = await request.GetRequestStreamAsync())
            {
                requestStream.Write(bytes, 0, bytes.Length);
            }

            // Send the request to the server and wait for the response:
            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            {
                var statusCode = response.StatusCode;
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    string x = reader.ReadToEnd();
                }
            }
        }
    }
}
