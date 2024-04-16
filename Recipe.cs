using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Recipe
{
    public string Name { get; private set; } = "";
    public string[] Ingredients { get; private set; } = Array.Empty<string>();
    public string[] Steps { get; private set; } = Array.Empty<string>();
    public double[] Quantities { get; private set; } = Array.Empty<double>();
    public double[] OriginalQuantities { get; private set; } = Array.Empty<double>(); // Added field to store original quantities
    public string[] Units { get; private set; } = Array.Empty<string>();

    public void EnterRecipe()
    {
        Console.WriteLine(new string('-', 30)); // Line at the top of the recipe entry
        Console.WriteLine("\nEnter recipe details:");

        Console.Write("Recipe name: ");
        Name = Console.ReadLine() ?? "";

        Console.Write("Number of ingredients: ");
        int numIngredients;
        while (!int.TryParse(Console.ReadLine(), out numIngredients) || numIngredients <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid input. Please enter a valid positive integer: ");
            Console.ResetColor();
        }

        Ingredients = new string[numIngredients];
        Quantities = new double[numIngredients];
        OriginalQuantities = new double[numIngredients]; // Initialize original quantities array
        Units = new string[numIngredients];
        for (int i = 0; i < numIngredients; i++)
        {
            Console.Write("Name of ingredient {0}: ", i + 1);
            Ingredients[i] = Console.ReadLine() ?? "";

            Console.Write("Quantity of ingredient {0}: ", i + 1);
            while (!double.TryParse(Console.ReadLine(), out Quantities[i]) || Quantities[i] <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Invalid input. Please enter a valid positive number: ");
                Console.ResetColor();
            }

            // Store original quantities
            OriginalQuantities[i] = Quantities[i];

            Console.Write("Unit of measurement for ingredient {0}: ", i + 1);
            Units[i] = Console.ReadLine() ?? "";
        }

        Console.Write("Number of steps: ");
        int numSteps;
        while (!int.TryParse(Console.ReadLine(), out numSteps) || numSteps <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid input. Please enter a valid positive integer: ");
            Console.ResetColor();
        }

        Steps = new string[numSteps];
        for (int i = 0; i < numSteps; i++)
        {
            Console.Write("Step {0}: ", i + 1);
            Steps[i] = Console.ReadLine() ?? "";
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Recipe entered successfully.");
        Console.ResetColor();
        Display();
    }

    public void Display()
    {
        Console.WriteLine(new string('-', 30)); // Line at the bottom of the recipe
        Console.WriteLine("\nRecipe: " + Name);

        Console.WriteLine("\nIngredients:");
        for (int i = 0; i < Ingredients.Length; i++)
        {
            Console.WriteLine($"- {Quantities[i]} {Units[i]} {Ingredients[i]}");
        }
        Console.WriteLine();

        Console.WriteLine("Steps:");
        for (int i = 0; i < Steps.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Steps[i]}");
        }
        Console.WriteLine(new string('-', 30)); // Line at the bottom of the recipe
    }

    public void ScaleRecipe()
    {
        if (Ingredients.Length == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nError: No recipe entered yet. Please enter a recipe first.");
            Console.ResetColor();
            return;
        }

        Console.Write("\nEnter scaling factor (0.5, 2, or 3): ");
        double factor;
        while (!double.TryParse(Console.ReadLine(), out factor) || (factor != 0.5 && factor != 2 && factor != 3))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid input. Please enter a valid scaling factor (0.5, 2, or 3): ");
            Console.ResetColor();
        }

        for (int i = 0; i < Quantities.Length; i++)
        {
            Quantities[i] *= factor;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Recipe scaled successfully.");
        Console.ResetColor();
        Display();
    }

    public void ResetQuantities()
    {
        if (Ingredients.Length == 0 || OriginalQuantities.Length == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nError: No recipe entered yet. Please enter a recipe first.");
            Console.ResetColor();
            return;
        }

        // Reset quantities to original values
        for (int i = 0; i < Quantities.Length; i++)
        {
            Quantities[i] = OriginalQuantities[i];
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Quantities reset to original values.");
        Console.ResetColor();
        Display();
    }

    public void ClearRecipeData()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\nAre you sure you want to clear recipe data? (yes/no): ");
        string confirm = Console.ReadLine()?.ToLower() ?? "";

        if (confirm == "yes")
        {
            Name = "";
            Ingredients = Array.Empty<string>();
            Steps = Array.Empty<string>();
            Quantities = Array.Empty<double>();
            OriginalQuantities = Array.Empty<double>(); // Reset original quantities array
            Units = Array.Empty<string>();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Recipe data cleared.");
        }
        else if (confirm == "no")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Operation canceled. Recipe data not cleared.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
        }

        Console.ResetColor();
    }


}
