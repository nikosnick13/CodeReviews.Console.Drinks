using static System.Console;


namespace DrinksInfo;

class Program
{
    static void Main(string[] args)
    { 
        UserInput userInput = new();
        userInput.GetCategoriesInput();
        ReadKey();
    }
}
