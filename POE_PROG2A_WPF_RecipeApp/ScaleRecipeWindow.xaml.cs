using System;
using System.Globalization;
using System.Windows;

namespace RecipeApp
{
    public partial class ScaleRecipeWindow : Window
    {
        private Recipe recipe;

        public ScaleRecipeWindow(Recipe recipe)
        {
            InitializeComponent();
            this.recipe = recipe;
        }

        private void ScaleButton_Click(object sender, RoutedEventArgs e)
        {
            // Use invariant culture to ensure the decimal point is correctly recognized
            if (double.TryParse(ScalingFactorTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double factor) && (factor == 0.5 || factor == 2 || factor == 3))
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    ingredient.Quantity = ingredient.OriginalQuantity * factor;
                }
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid scaling factor (0.5, 2, or 3).");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
