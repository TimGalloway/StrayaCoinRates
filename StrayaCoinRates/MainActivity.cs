using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.Json;
using System.Net;
using System;
using System.IO;

namespace StrayaCoinRates
{
    [Activity(Label = "StrayaCoinRates", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            button.Click += async delegate
            {
                string url = "https://api.strayawallet.com/rates/";
                JsonValue json = await FetchRatesAsync(url);
                ParseAndDisplay(json);
            };
        }

        private void ParseAndDisplay(JsonValue json)
        {
            throw new NotImplementedException();
        }

        private async Task<JsonValue> FetchRatesAsync (string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }
    }
}

