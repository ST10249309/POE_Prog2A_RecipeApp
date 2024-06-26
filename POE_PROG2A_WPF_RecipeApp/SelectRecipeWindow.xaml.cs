using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeApp
{
    public partial class SelectRecipeWindow : Window
    {
        public Recipe SelectedRecipe { get; private set; }
        private List<Recipe> recipes;

        public SelectRecipeWindow(List<Recipe> recipes, string action)
        {
            InitializeComponent();
            Title = action;
            this.recipes = recipes;

            // Sort recipes alphabetically and indicate if they are over 300 calories
            var sortedRecipes = recipes
                .OrderBy(r => r.Name)
                .Select(r => new
                {
                    Recipe = r,
                    DisplayName = r.Name + (r.CalculateTotalCalories() > 300 ? " (Over 300 Calories)" : "")
                }).ToList();

            RecipeListBox.ItemsSource = sortedRecipes;
            RecipeListBox.DisplayMemberPath = "DisplayName";
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeListBox.SelectedItem != null)
            {
                SelectedRecipe = ((dynamic)RecipeListBox.SelectedItem).Recipe;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a recipe.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
