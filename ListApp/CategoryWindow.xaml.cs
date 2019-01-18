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
    /// Logika interakcji dla klasy CategoryWindow.xaml
    /// </summary>
    public partial class CategoryWindow : Window
    {
        Data data;

        public CategoryWindow(object dataContext)
        {
            InitializeComponent();
            data = (Data)dataContext;
            DataContext = data;
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if(CategoryNameTextBox.Text.Trim() != "")
            {
                data.AddCategory(CategoryNameTextBox.Text);
                CategoryNameTextBox.Clear();
            }
        }

        private void RemoveCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryListView.SelectedItem != null)
            {
                if(MessageBox.Show("Czy na pewno chcesz usunąć kategorie? Wraz z nią zostaną usunięte wszystkie powiązane wpisy","Ostrzeżenie", MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    data.RemoveCategory(CategoryListView.SelectedItem.ToString());                  
                }
            }

        }


        private void MoveUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryListView.SelectedItem != null)
                data.MoveCategoryUp(CategoryListView.SelectedIndex);
        }

        private void MoveDownButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryListView.SelectedItem != null)
                data.MoveCategoryDown(CategoryListView.SelectedIndex);
        }

    }
}
