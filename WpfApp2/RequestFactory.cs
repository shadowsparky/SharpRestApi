using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    public class RequestFactory<T> where T: Response
    {
        public delegate void RequestCallback(List<T> result);
        private static readonly HttpClient client = new HttpClient();

        public async void GetRequest(String url, RequestCallback callback)
        {
            try
            {
                var response = await client.GetStringAsync(url);
                callback.Invoke(JsonConverter(response));
            }
            catch (HttpRequestException)
            {
                callback.Invoke(null);
            }
        }

        public async void PostRequest(String url, Dictionary<string, string> arguments, RequestCallback callback)
        {
            try
            {
                var content = new FormUrlEncodedContent(arguments);
                var response = await client.PostAsync(url, content);
                var responseStr = await response.Content.ReadAsStringAsync();
                callback.Invoke(JsonConverter(responseStr));        
            }
            catch (HttpRequestException)
            {
                callback.Invoke(null);
            }
        }

        private List<T> JsonConverter(string response)
        {
            List<T> result;
            result = JsonConvert.DeserializeObject<List<T>>(response);
            return result; 
        }
    }
}
