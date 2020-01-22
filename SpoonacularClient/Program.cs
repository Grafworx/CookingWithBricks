using Bricks.Data;
using Bricks.Model;
using com.spoonacular;
using Newtonsoft.Json;
using Org.OpenAPITools.Client;
using RestSharp;
using System;
using System.Collections.Generic;

namespace SpoonacularClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            BricksContext context = new BricksContext();
            context.Database.EnsureCreated();

            User user = new User { Name = "Alastair P", Username = "pandelus" };
            context.Users.Add(user);
            context.SaveChanges();

            /*ApiClient apiClient = new ApiClient();
            DefaultApi api = new DefaultApi(apiClient);

            string spoonacular = Environment.GetEnvironmentVariable("spoonacular");

            var request = new RestRequest($"/recipes/extract?url=https://www.bbcgoodfood.com/recipes/cinnamon-berry-granola-bars/&apiKey={spoonacular}", Method.GET);

            var json = new RestClient("https://api.spoonacular.com").Execute(request).Content;

            Recipe recipe = JsonConvert.DeserializeObject<Recipe>(json);*/
        }
    }
}
