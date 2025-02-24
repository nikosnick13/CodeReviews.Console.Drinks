using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using static System.Console;
using System.Web;
using DrinksInfo.Models;
using Newtonsoft.Json;
using RestSharp;

namespace DrinksInfo;

public class DrinksService
{
    public List<Category> GetCategories()
    {
        var client = new RestClient("https://www.thecocktaildb.com/api/json/v1/1/"); //RestClient: Δημιουργεί έναν client για να στείλει το HTTP αίτημα στο API. 
        var request = new RestRequest("list.php?c=list"); //RestRequest: Δημιουργεί το συγκεκριμένο αίτημα (δηλαδή, το endpoint που θέλουμε να καλέσουμε).
        var response = client.ExecuteAsync(request); //ExecuteAsync: Εκτελεί το αίτημα ασύγχρονα και επιστρέφει την απάντηση του server.

        List<Category> categories = new();

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string? rawResponse = response.Result.Content;
            var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);

           categories = serialize.CategoriesList;
            TableVisualizationEngine.ShowMenu(categories, "Categories");

            return categories;
        }
        return categories;
    }

    public List<Drink>  GetDrinksByCategory(string category)
    {
        var client = new RestClient("https://www.thecocktaildb.com/api/json/v1/1/");
        var request = new RestRequest($"filter.php?c={HttpUtility.UrlEncode(category)}");
        var response = client.ExecuteAsync(request);
        List<Drink> drinks = new();

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string? rowResponse = response.Result.Content;
            var serialize = JsonConvert.DeserializeObject<Drinks>(rowResponse);

           drinks = serialize.DrinksList;
            TableVisualizationEngine.ShowMenu(drinks, "Drinks Menu");
            return drinks;
        }
        return drinks;
    }

    internal void GetDrink(string drink)
    {
        var client = new RestClient("https://www.thecocktaildb.com/api/json/v1/1/");
        var request = new RestRequest($"lookup.php?i={drink}");
        var response = client.ExecuteAsync(request);

        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string? rawResponse = response.Result.Content;
             
            var serialize = JsonConvert.DeserializeObject<DrinkDetailObject>(rawResponse);

            List<DrinkDetail> returnList = serialize.DrinkDetailList;

            if (returnList != null && returnList.Count > 0)
            {
                DrinkDetail drinkDetail = returnList[0];

                List<object> prepLisr = new();

                string formaantName = "";

                foreach (PropertyInfo prop in drinkDetail.GetType().GetProperties())
                {

                    if (prop.Name.Contains("str"))
                    {
                        formaantName = prop.Name.Substring(3);
                    }
                    if (!string.IsNullOrEmpty(prop.GetValue(drinkDetail)?.ToString()))
                    {

                        prepLisr.Add(new
                        {
                            Name = formaantName,
                            Value = prop.GetValue(drinkDetail)
                        });
                    }
                }
                TableVisualizationEngine.ShowMenu(prepLisr, "Drink Details");
            }
            else WriteLine("⚠️ No drink details found!");
                
            

               
           
        }
    }
}
