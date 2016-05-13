using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmartHomeControl
{
    public partial class SmartHomePage : ContentPage
    {
        public SmartHomePage()
        {
            InitializeComponent();
        }

        void OnSliderValueChanged(object sender,
                          ValueChangedEventArgs args)
        {
            valueLabel.Text = args.NewValue.ToString("F3");
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            SmartHomeService service = SmartHomeService.Instance;
            string state = await service.FetchItemState("Light_FF_Office_Ceiling");

            if(state == "ON")
            {
                await service.PutItemState("Light_FF_Office_Ceiling", "OFF");
            }
            else
            {
                await service.PutItemState("Light_FF_Office_Ceiling", "ON");
            }

            valueLabel.Text = state;

      /*    Button button = (Button)sender;
            await DisplayAlert("Clicked!",
                "The button labeled '" + button.Text + "' has been clicked",
                "OK");*/
        }

        private async Task<string> FetchItemState(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
//            request.ContentType = "application/json";
            request.ContentType = "text/plain";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();

                //    var results = JsonConvert.DeserializeObject<dynamic>(content);

                    // Use this stream to build a JSON document object:
                   // JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                  //  var rootobject = JsonConvert. DeserializeObject<Rootobject>(earthquakesJson);
                    // Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return content;
                }
            }
        }

    }
}
