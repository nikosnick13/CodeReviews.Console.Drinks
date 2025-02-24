using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinksInfo;

public class Validation
{
    public static bool IsStringValid(string input)
    {
        // Αν η είσοδος είναι null ή κενή, επιστρέφει false
        if (string.IsNullOrEmpty(input)) return false;

        // Διατρέχει κάθε χαρακτήρα στην είσοδο
        foreach (char c in input)
        {
            // Αν ο χαρακτήρας δεν είναι γράμμα και δεν είναι '/' ή ένα κενό διάστημα, επιστρέφει false
            if (!char.IsLetter(c) && c != '/' && c != ' ')
                return false;
        }

        return true;
    }

    public static bool IsNumberValid(string drink)
    {
        if (String.IsNullOrEmpty(drink)) return false;

        foreach (char c in drink)
        {
            // Αν ο χαρακτήρας δεν είναι ψηφίο, επιστρέφει false
            if (!char.IsDigit(c))
                return false;
        }
        return true;

    }
}
