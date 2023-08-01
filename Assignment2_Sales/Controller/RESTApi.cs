using Assignment2_Sales.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Assignment2_Sales.Controller
{
    internal class RESTApi
    {
        static HttpClient client = new HttpClient();

        public static async void updateProduct(Models.Product product, int weight)
        {
            client.BaseAddress = new Uri("https://localhost:7159/Api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var res = await client.PutAsJsonAsync("UpdateProduct", new Models.Product(product, weight));

        }
    }
}
