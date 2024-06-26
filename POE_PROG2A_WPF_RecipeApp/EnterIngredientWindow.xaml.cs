using System.Windows;

namespace RecipeApp
{
    public partial class EnterIngredientWindow : Window
    {
        public Ingredient Ingredient { get; private set; }

        public EnterIngredientWindow()
        {
            InitializeComponent();
            Ingredient = new Ingredient();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Ingredient.Name = IngredientNameTextBox.Text;
            Ingredient.Quantity = double.Parse(QuantityTextBox.Text);
            Ingredient.OriginalQuantity = Ingredient.Quantity;
            Ingredient.Unit = UnitTextBox.Text;
            Ingredient.Calories = double.Parse(CaloriesTextBox.Text);
            Ingredient.FoodGroup = (FoodGroup)FoodGroupComboBox.SelectedIndex;

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
