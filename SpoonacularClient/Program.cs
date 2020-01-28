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

            //SeedData();
        }

        private static void SeedData()
        {
            using (BricksContext context = new BricksContext())
            {
                User user = new User { Name = "Alastair P", Username = "pandelus" };
                context.Users.Add(user);
                context.SaveChanges();

                ApiClient apiClient = new ApiClient();
                DefaultApi api = new DefaultApi(apiClient);

                string spoonacular = Environment.GetEnvironmentVariable("spoonacular");

                string[] recipeAddresses = new string[]
                {
                "https://www.jamieoliver.com/recipes/pasta-recipes/a-killer-mac-n-cheese/",
                "https://www.vegrecipesofindia.com/aloo-tikki-aloo-pattice-recipe-made-from-leftover-potatoes/#wprm-recipe-container-134393",
                "https://www.arancinotto.com/blogs/ricette-e-news/ricetta-arancini-al-ragu",
                "http://allrecipes.co.uk/recipe/6766/basic-flapjacks.aspx",
                "https://minimalistbaker.com/easy-chana-masala/",
                "https://www.bbcgoodfood.com/recipes/2920672/chicken-and-leek-pie",
                "https://www.bbcgoodfood.com/recipes/one-pot-chicken-chasseur",
                "http://sharacooks.blogspot.com/2010/12/chicken-korma-curry-my-children-will.html",
                "http://allrecipes.co.uk/recipe/6183/choc-chip-loaf.aspx",
                "https://www.bbcgoodfood.com/recipes/cinnamon-berry-granola-bars",
                "https://www.thekitchn.com/recipe-cinnamonspiked-tomatoes-48694",
                "http://frugalfeeding.com/2012/04/19/classic-caraway-seed-cake/",
                "https://www.bbcgoodfood.com/recipes/coconut-fish-curry",
                "https://www.bbcgoodfood.com/recipes/cottage-pie",
                "https://www.bbc.co.uk/food/recipes/courgette_and_caraway_84359",
                "https://www.bbcgoodfood.com/recipes/creamy-mushroom-soup",
                "https://www.vegrecipesofindia.com/restaurant-style-dal-tadka/",
                "https://minimalistbaker.com/dark-chocolate-hemp-energy-bites/",
                "https://www.yummly.co.uk/recipe/Farro-And-Roasted-Red-Pepper-Salad-2356108",
                "https://www.bbcgoodfood.com/recipes/fruity-lamb-tagine",
                "https://www.bbcgoodfood.com/recipes/2090637/ginger-cake-#",
                "https://www.bbc.co.uk/food/recipes/harissa_baked_fish_with_63091",
                "https://minimalistbaker.com/healthy-5-ingredient-granola-bars/",
                "https://www.jamieoliver.com/recipes/fish-recipes/fish-pie/",
                "https://www.greatbritishchefs.com/recipes/lamb-barbacoa-recipe",
                "https://www.bbcgoodfood.com/recipes/leek-bacon-potato-soup",
                "https://www.bbcgoodfood.com/recipes/lemon-drizzle-slices",
                "https://www.bbcgoodfood.com/recipes/lentil-bacon-soup",
                "http://www.lupepintos.com/lupe-pintos-famous-chile-con-carne",
                "https://www.bbc.co.uk/food/recipes/marys_florentines_49833",
                "http://www.slowburningpassion.com/moroccan-style-salmon-with-olives-and-preserved-lemon/",
                "https://www.bbcgoodfood.com/recipes/no-fuss-shepherds-pie",
                "https://www.leaf.tv/13718590/spanish-paella-recipe/",
                "https://www.harighotra.co.uk/paneer-makhani-recipe",
                "https://www.aninas-recipes.com/recipes/paprika-beef-tomato-mushroom-stew/",
                "https://www.pataks.co.uk/recipes/prawns-in-coconut",
                "https://www.lifestylefood.com.au/recipes/3791/moroccan-lamb-tagine-with-buttered-couscous",
                "https://www.bbcgoodfood.com/recipes/1977654/roast-red-pepper-and-tomato-soup",
                "https://www.jamieoliver.com/recipes/pork-recipes/sausage-rolls/",
                "https://www.epicurious.com/recipes/food/views/sausages-with-white-beans-in-tomato-sauce-355202",
                "https://www.simplyscratch.com/2014/05/slow-cooker-chipotle-honey-pulled-pork.html",
                "https://vikalinka.com/crock-pot-rustic-italian-beef-ragu/",
                "https://www.bbcgoodfood.com/recipes/spiced-carrot-lentil-soup",
                "https://www.foodrepublic.com/recipes/spiced-coconut-lentil-soup-recipe/",
                "https://thefoodmedic.co.uk/2017/10/spicy-butternut-squash-and-carrot-soup/",
                "http://www.puttanescasauce.com/",
                "http://allrecipes.co.uk/recipe/36924/tomato-and-basil-soup-with-a-kick.aspx?o_is=LV",
                "https://www.greatbritishchefs.com/recipes/roast-turkey-recipe-with-apricot-stuffing",
                "https://www.bbcgoodfood.com/recipes/yummy-scrummy-carrot-cake-recipe"
                };

                foreach (string recipeAddress in recipeAddresses)
                {
                    var request = new RestRequest($"/recipes/extract?url={recipeAddress}&apiKey={spoonacular}", Method.GET);

                    var json = new RestClient("https://api.spoonacular.com").Execute(request).Content;

                    SpoonacularModel.Recipe spoonacularRecipe = JsonConvert.DeserializeObject<SpoonacularModel.Recipe>(json);

                    Recipe recipe = new Recipe
                    {
                        Name = spoonacularRecipe.title,
                        Detail = json,
                        User = user
                    };

                    context.Add<Recipe>(recipe);
                    context.SaveChanges();
                }
            }
        }
    }
}
