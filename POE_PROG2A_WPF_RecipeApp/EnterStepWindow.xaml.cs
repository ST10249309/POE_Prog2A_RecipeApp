using System.Windows;

namespace RecipeApp
{
    public partial class EnterStepWindow : Window
    {
        public string Step { get; private set; }

        public EnterStepWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(StepTextBox.Text))
            {
                Step = StepTextBox.Text;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Step description cannot be empty.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
