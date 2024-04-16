using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Welcome to the Recipe App!");
        Console.ResetColor();

        Recipe recipe = new Recipe();

        while (true)
        {
            Console.WriteLine(new string('-', 30)); // Line at the top of the menu
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Menu:");
            Console.ForegroundColor = ConsoleColor.Cyan; // Change color for each option
            Console.WriteLine("1. Enter recipe");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("2. Scale recipe");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("3. Reset quantities");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("4. Clear recipe data");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("5. Exit");
            Console.ResetColor();
            Console.WriteLine(new string('-', 30)); // Line at the bottom of the menu
            Console.Write("\nEnter your choice: ");
            string? choice = Console.ReadLine();


            switch (choice)
            {
                case "1":
                    recipe.EnterRecipe();
                    break;
                case "2":
                    recipe.ScaleRecipe();
                    break;
                case "3":
                    recipe.ResetQuantities();
                    break;
                case "4":
                    recipe.ClearRecipeData();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid choice. Please enter a number between 1 and 5.");
                    Console.ResetColor();
                    break;
            }
        }
    }
}


