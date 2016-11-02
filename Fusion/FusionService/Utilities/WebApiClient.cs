using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Newtonsoft.Json;

namespace FusionService.Utilities
{
    class WebApiClient
    {
        private HttpClient client = null;

        public WebApiClient(string uriString)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(uriString);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Uri> Post<T>(string resourceUri, T obj)
        {
            //HttpResponseMessage response = await client.PostAsJsonAsync(resourceUri, obj);

            JsonMediaTypeFormatter formatter = new JsonMediaTypeFormatter();
            formatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            HttpResponseMessage response = await client.PostAsync(resourceUri, obj, formatter);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }
    }
}
