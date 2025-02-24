using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using System.Threading.Tasks;

namespace DrinksInfo;


public class UserInput
{
    DrinksService drinksService = new();

    public void GetCategoriesInput()
    {

        drinksService.GetCategories();

        WriteLine("Please select a category from the list above:");

        string? category = ReadLine();

        while (!Validation.IsStringValid(category))
        {
            WriteLine("Invalid input. Please select a category from the list above:");
            category = ReadLine();
        }

        GetDrinksInput(category);

    }

    private void GetDrinksInput(string category)
    {
      var drinks = drinksService.GetDrinksByCategory(category);

        WriteLine("Choose a drink or go back to category menu by typing 0:");

        string drink = ReadLine();

        if (drink == "0") GetCategoriesInput();
        
        while (!Validation.IsNumberValid(drink))
        {
            WriteLine("\nInvalid drink");
            drink = ReadLine();
        }

        if (!drinks.Any(x => x.idDrink == drink))
        {
            WriteLine("Drink dosen't exist.");
            GetDrinksInput(category);
        }

        drinksService.GetDrink(drink);

        WriteLine("Press any key to go back to categories menu");
        ReadKey();
        if (!Console.KeyAvailable) GetCategoriesInput();

    }


}