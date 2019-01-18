using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ListApp
{
    /// <summary>
    /// Logika interakcji dla klasy FoodWindow.xaml
    /// </summary>
    public partial class FoodWindow : Window
    {
        Data data;
        Food foodToEdit;

        public FoodWindow(object dataContext, Food foodToEdit = null)
        {
            data = (Data)dataContext;
            this.DataContext = data;
            InitializeComponent();

            if(foodToEdit != null)
            {
                this.foodToEdit = foodToEdit;
                AddFoodButton.Content = "Edytuj";
                FoodNameTextBox.Text = foodToEdit.Name;
                IsGoodRadioButton.IsChecked = foodToEdit.IsGood;
                IsNotGoodRadioButton.IsChecked = !IsGoodRadioButton.IsChecked;
                CategoryComboBox.Text = foodToEdit.Category;
            }

        }

        private void AddFoodButton_Click(object sender, RoutedEventArgs e)
        {
            if(foodToEdit == null)
            {
                data.Foods.Add(new Food(Food.LastID + 1, FoodNameTextBox.Text, CategoryComboBox.Text, IsGoodRadioButton.IsChecked == true, DateTime.Now));
                FoodNameTextBox.Text = string.Empty;
            }
            else
            {
                foodToEdit.Name = FoodNameTextBox.Text;
                foodToEdit.Category = CategoryComboBox.Text;
                foodToEdit.IsGood = IsGoodRadioButton.IsChecked == true;
                foodToEdit.CreationDate = DateTime.Now;
                this.Close();
            }
        }
    }
}
