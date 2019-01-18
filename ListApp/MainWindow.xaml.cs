using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ListApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Data data = new Data();


        #region Constructor
        public MainWindow()
        {
            DataContext = data;
            data.Load();
            InitializeComponent();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(FoodListView.ItemsSource);
            view.Filter = FoodListViewFilter;
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            
            foreach (var sort in view.SortDescriptions)
            {
                Console.WriteLine(sort);
            }

        }
        #endregion

        #region ListViewMethods

        private bool FoodListViewFilter(object item)
        {
            bool statement = true;

            if(!string.IsNullOrEmpty(SearchTextBox.Text))
                statement &= ((item as Food).Name.IndexOf(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);

            if (CategoryComboBox.SelectedItem.ToString() != "Wszystkie")
                statement &= ((item as Food).Category.Equals(CategoryComboBox.SelectedItem.ToString()));

            if (ShowAllRadioButton.IsChecked == false)
                statement &= ((item as Food).IsGood == ShowGoodRadioButton.IsChecked);

            return statement;
        }

        private void UpdateFoodListView()
        {
            CollectionViewSource.GetDefaultView(FoodListView.ItemsSource).Refresh();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateFoodListView();
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFoodListView();
            Console.WriteLine(CategoryComboBox.Text);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateFoodListView();
        }

#endregion

        private void Window_Closed(object sender, EventArgs e)
        {
            data.Save();
        }

        private void AddFoodButton_Click(object sender, RoutedEventArgs e)
        {
            FoodWindow window = new FoodWindow(this.DataContext);
            window.ShowDialog();
        }

        private void CategoryManagementButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow window = new CategoryWindow(this.DataContext);
            window.ShowDialog();
        }

        private void EditFoodMenuItem_Click(object sender, RoutedEventArgs e)
        {
            FoodWindow window = new FoodWindow(DataContext, (Food)FoodListView.SelectedItems[0]);
            window.ShowDialog();
            UpdateFoodListView();
        }

        private void RemoveFoodMenuItem_Click(object sender, RoutedEventArgs e)
        {
            data.Foods.Remove((Food)FoodListView.SelectedItems[0]);
            UpdateFoodListView();
        }
    }
}
