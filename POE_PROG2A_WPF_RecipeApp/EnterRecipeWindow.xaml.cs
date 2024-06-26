using System.Collections.Generic;
using System.Windows;

namespace RecipeApp
{
    public partial class EnterRecipeWindow : Window
    {
        private Recipe recipe;

        public EnterRecipeWindow(Recipe recipe)
        {
            InitializeComponent();
            this.recipe = recipe;
        }

        private void AddIngredientsButton_Click(object sender, RoutedEventArgs e)
        {
            int numIngredients;
            if (int.TryParse(NumIngredientsTextBox.Text, out numIngredients) && numIngredients > 0)
            {
                recipe.Ingredients.Clear();
                for (int i = 0; i < numIngredients; i++)
                {
                    var ingredientWindow = new EnterIngredientWindow();
                    if (ingredientWindow.ShowDialog() == true)
                    {
                        recipe.Ingredients.Add(ingredientWindow.Ingredient);
                    }
                }
                UpdateIngredientList();
            }
            else
            {
                MessageBox.Show("Please enter a valid number of ingredients.");
            }
        }

        private void AddStepsButton_Click(object sender, RoutedEventArgs e)
        {
            int numSteps;
            if (int.TryParse(NumStepsTextBox.Text, out numSteps) && numSteps > 0)
            {
                recipe.Steps.Clear();
                for (int i = 0; i < numSteps; i++)
                {
                    var stepWindow = new EnterStepWindow();
                    if (stepWindow.ShowDialog() == true)
                    {
                        recipe.Steps.Add(stepWindow.Step);
                    }
                }
                UpdateStepList();
            }
            else
            {
                MessageBox.Show("Please enter a valid number of steps.");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(RecipeNameTextBox.Text))
            {
                recipe.Name = RecipeNameTextBox.Text;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Recipe name cannot be empty.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void UpdateIngredientList()
        {
            IngredientsListBox.Items.Clear();
            foreach (var ingredient in recipe.Ingredients)
            {
                IngredientsListBox.Items.Add($"{ingredient.Quantity} {ingredient.Unit} {ingredient.Name}");
            }
        }

        private void UpdateStepList()
        {
            StepsListBox.Items.Clear();
            foreach (var step in recipe.Steps)
            {
                StepsListBox.Items.Add(step);
            }
        }
    }
}
