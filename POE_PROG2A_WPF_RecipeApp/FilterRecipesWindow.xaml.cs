using System;
using System.Windows;

namespace RecipeApp
{
    public partial class FilterRecipesWindow : Window
    {
        public string IngredientName { get; private set; }
        public FoodGroup? SelectedFoodGroup { get; private set; }
        public double? MaxCalories { get; private set; }

        public FilterRecipesWindow()
        {
            InitializeComponent();
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            IngredientName = IngredientNameTextBox.Text;

            if (FoodGroupComboBox.SelectedIndex != -1)
            {
                SelectedFoodGroup = (FoodGroup)FoodGroupComboBox.SelectedIndex;
            }

            if (double.TryParse(MaxCaloriesTextBox.Text, out double maxCalories))
            {
                MaxCalories = maxCalories;
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
