using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeApp
{
    public delegate void CalorieNotificationHandler(string message);

    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();
        public List<string> Steps { get; private set; } = new List<string>();
        public event CalorieNotificationHandler? CalorieNotification;

        public void EnterRecipe()
        {
            var window = new EnterRecipeWindow(this);
            window.ShowDialog();

            double totalCalories = CalculateTotalCalories();
            if (totalCalories > 300)
            {
                CalorieNotification?.Invoke("Warning: The total calories of this recipe exceed 300. Calories measure the energy supplied by food and drinks.");
            }
        }

        public void Display()
        {
            string message = $"Recipe: {Name}\n\nIngredients:\n{string.Join("\n", Ingredients.Select(i => $"- {i.Quantity} {i.Unit} {i.Name} ({i.Calories} calories, {i.FoodGroup})"))}\n\nSteps:\n";

            for (int i = 0; i < Steps.Count; i++)
            {
                message += $"{i + 1}. {Steps[i]}\n";
            }

            message += $"\nTotal Calories: {CalculateTotalCalories()}";
            if (CalculateTotalCalories() > 300)
            {
                message += "\nWarning: This recipe exceeds 300 calories.";
            }

            MessageBox.Show(message);
        }

        public void ScaleRecipe()
        {
            if (!Ingredients.Any())
            {
                MessageBox.Show("Error: No recipe entered yet. Please enter a recipe first.");
                return;
            }

            var window = new ScaleRecipeWindow(this);
            if (window.ShowDialog() == true)
            {
                double totalCalories = CalculateTotalCalories();
                if (totalCalories > 300)
                {
                    CalorieNotification?.Invoke("Warning: The total calories of this recipe exceed 300. Calories measure the energy supplied by food and drinks.");
                }
            }
        }

        public void ResetQuantities()
        {
            if (!Ingredients.Any())
            {
                MessageBox.Show("Error: No recipe entered yet. Please enter a recipe first.");
                return;
            }

            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity = ingredient.OriginalQuantity;
            }

            MessageBox.Show("Quantities reset to original values.");
        }

        public double CalculateTotalCalories()
        {
            return Ingredients.Sum(ingredient => ingredient.Calories * ingredient.Quantity / ingredient.OriginalQuantity);
        }
    }

    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double OriginalQuantity { get; set; }
        public string Unit { get; set; }
        public double Calories { get; set; }
        public FoodGroup FoodGroup { get; set; }
    }

    public enum FoodGroup
    {
        StarchyFoods,
        VegetablesAndFruits,
        DryBeansPeasLentilsAndSoya,
        ChickenFishMeatAndEggs,
        MilkAndDairyProducts,
        FatsAndOil,
        Water
    }
}
