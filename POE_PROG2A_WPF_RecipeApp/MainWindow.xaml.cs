using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes = new List<Recipe>();

        public MainWindow()
        {
            InitializeComponent();
            EnterRecipeButton.Click += EnterRecipeButton_Click;
            ScaleRecipeButton.Click += ScaleRecipeButton_Click;
            ResetQuantitiesButton.Click += ResetQuantitiesButton_Click;
            ClearRecipeDataButton.Click += ClearRecipeDataButton_Click;
            ListAllRecipesButton.Click += ListAllRecipesButton_Click;
            FilterRecipesButton.Click += FilterRecipesButton_Click;
            ExitButton.Click += ExitButton_Click;
        }

        private void EnterRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            var recipe = new Recipe();
            recipe.CalorieNotification += message => MessageBox.Show(message);
            var enterRecipeWindow = new EnterRecipeWindow(recipe);
            if (enterRecipeWindow.ShowDialog() == true)
            {
                recipes.Add(recipe);
                UpdateRecipeList();
            }
        }

        private void ScaleRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!recipes.Any())
            {
                MessageBox.Show("No recipes available. Please enter a recipe first.");
                return;
            }

            var selectRecipeWindow = new SelectRecipeWindow(recipes, "Scale Recipe");
            if (selectRecipeWindow.ShowDialog() == true)
            {
                var selectedRecipe = selectRecipeWindow.SelectedRecipe;
                selectedRecipe.ScaleRecipe();
                UpdateRecipeList();
            }
        }

        private void ResetQuantitiesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!recipes.Any())
            {
                MessageBox.Show("No recipes available. Please enter a recipe first.");
                return;
            }

            var selectRecipeWindow = new SelectRecipeWindow(recipes, "Reset Quantities");
            if (selectRecipeWindow.ShowDialog() == true)
            {
                var selectedRecipe = selectRecipeWindow.SelectedRecipe;
                selectedRecipe.ResetQuantities();
                UpdateRecipeList();
            }
        }

        private void ClearRecipeDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (!recipes.Any())
            {
                MessageBox.Show("No recipes available to clear.");
                return;
            }

            var selectRecipeWindow = new SelectRecipeWindow(recipes, "Clear Recipe Data");
            if (selectRecipeWindow.ShowDialog() == true)
            {
                var selectedRecipe = selectRecipeWindow.SelectedRecipe;
                recipes.Remove(selectedRecipe);
                UpdateRecipeList();
            }
        }

        private void ListAllRecipesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!recipes.Any())
            {
                MessageBox.Show("No recipes available.");
                return;
            }

            var selectRecipeWindow = new SelectRecipeWindow(recipes, "Display Recipe");
            if (selectRecipeWindow.ShowDialog() == true)
            {
                var selectedRecipe = selectRecipeWindow.SelectedRecipe;
                selectedRecipe.Display();
            }
        }

        private void FilterRecipesButton_Click(object sender, RoutedEventArgs e)
        {
            var filterWindow = new FilterRecipesWindow();
            if (filterWindow.ShowDialog() == true)
            {
                var filteredRecipes = recipes.AsEnumerable();

                if (!string.IsNullOrWhiteSpace(filterWindow.IngredientName))
                {
                    filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.Name.Contains(filterWindow.IngredientName, StringComparison.OrdinalIgnoreCase)));
                }

                if (filterWindow.SelectedFoodGroup.HasValue)
                {
                    filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.FoodGroup == filterWindow.SelectedFoodGroup.Value));
                }

                if (filterWindow.MaxCalories.HasValue)
                {
                    filteredRecipes = filteredRecipes.Where(r => r.CalculateTotalCalories() <= filterWindow.MaxCalories.Value);
                }

                RecipeListBox.Items.Clear();
                foreach (var recipe in filteredRecipes.OrderBy(r => r.Name))
                {
                    RecipeListBox.Items.Add(recipe.Name + (recipe.CalculateTotalCalories() > 300 ? " (Over 300 Calories)" : ""));
                }
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void UpdateRecipeList()
        {
            RecipeListBox.Items.Clear();
            foreach (var recipe in recipes.OrderBy(r => r.Name))
            {
                RecipeListBox.Items.Add(recipe.Name + (recipe.CalculateTotalCalories() > 300 ? " (Over 300 Calories)" : ""));
            }
        }
    }
}
