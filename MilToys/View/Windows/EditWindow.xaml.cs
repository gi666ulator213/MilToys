using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MilToys.View.Windows {
    public partial class EditWindow : Window {
        public EditWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            var ages = DataAccess.LoadAgeLimits();

            foreach (var age in ages) {
                AddAgeLimitToList(age, ages.IndexOf(age));
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e) {
            FindParent<Grid>(sender as Button).Visibility = Visibility.Collapsed;

            string age = Convert.ToString((sender as Button).Name.Replace("DeleteCategory", ""));
            EditDB.DeleteAgeLimit(age);
        }

        public string? OpenedAgeLimit = null;

        private void EditCategory_Click(object sender, RoutedEventArgs e) {
            int ageIndex = Convert.ToInt32((sender as Button).Name.Replace("EditCategory", ""));
            var ages = DataAccess.LoadAgeLimits();
            ListHeader.Text = $"Список іграшок в віковій категорії: {ages[ageIndex]}";

            OpenedAgeLimit = ages[ageIndex];

            List.Children.Clear();

            foreach (var toy in DataAccess.LoadToysFromAgeLimit(ages[ageIndex]))
            {
                AddToyToList(toy.Name, toy.Id);
            }

            Add.Visibility = Visibility.Visible;
            Return.Visibility = Visibility.Visible;
        }

        private void DeleteToy_Click(object sender, RoutedEventArgs e) {
            FindParent<Grid>(sender as Button).Visibility = Visibility.Collapsed;

            int id = Convert.ToInt32((sender as Button).Name.Replace("DeleteToy", ""));
            EditDB.DeleteToy(id);
        }

        public Toy EditedToy;
        public Grid EditedToyGrid;

        private void EditToy_Click(object sender, RoutedEventArgs e) {
            EditedToyGrid = FindParent<Grid>(sender as Button);
            var editToyWindow = new EditToyWindow();
            editToyWindow.Owner = this;

            EditedToy = DataAccess.GetToyById(Convert.ToInt32((sender as Button).Name.Replace("EditToy", "")));
            editToyWindow.ToyName.Text = EditedToy.Name;
            editToyWindow.ToyStock.Text = EditedToy.Stock;
            editToyWindow.ToyAge.Text = EditedToy.Age.ToString();
            editToyWindow.ToyPrice.Text = EditedToy.Price.ToString();

            editToyWindow.ShowDialog();
        }

        private void Add_Click(object sender, RoutedEventArgs e) {
            var NtWindow = new NewToyWindow();
            NtWindow.Owner = this;
            NtWindow.ShowDialog();
        }

        public void Return_Click(object sender, RoutedEventArgs e) {
            ListHeader.Text = "Вікові категорії";

            List.Children.Clear();

            var ages = DataAccess.LoadAgeLimits();

            foreach (var age in ages) {
                AddAgeLimitToList(age, ages.IndexOf(age));
            }

            Return.Visibility = Visibility.Hidden;

            OpenedAgeLimit = null;
        }

        void AddAgeLimitToList(string age, int i) {
            Grid grid = new Grid();

            var textBlock = new TextBlock() { Style = (Style)FindResource("ListTextBlock") };
            textBlock.Text = "Категорія " + age;

            var deleteButton = new Button() { Style = (Style)FindResource("ButtonStyle") };
            deleteButton.Margin = new Thickness(10, 5, 10, 5);
            deleteButton.Name = "DeleteCategory" + i;
            deleteButton.Click += DeleteCategory_Click;

            var deleteIcon = new PackIcon() { Style = (Style)FindResource("PackIconStyle") };
            deleteIcon.Kind = PackIconKind.Delete;

            deleteButton.Content = deleteIcon;

            Button editButton = new Button() { Style = (Style)FindResource("ButtonStyle") };
            editButton.Margin = new Thickness(10, 5, 60, 5);
            editButton.Name = "EditCategory" + i;
            editButton.Click += EditCategory_Click;

            PackIcon editIcon = new PackIcon() { Style = (Style)FindResource("PackIconStyle") };
            editIcon.Kind = PackIconKind.Edit;

            editButton.Content = editIcon;

            grid.Children.Add(textBlock);
            grid.Children.Add(deleteButton);
            grid.Children.Add(editButton);

            List.Children.Add(grid);
        }

        public void AddToyToList(string name, int id) {
            Grid grid = new Grid();

            var textBlock = new TextBlock() { Style = (Style)FindResource("ListTextBlock") };
            textBlock.Text = "Іграшка " + name;

            var deleteButton = new Button() { Style = (Style)FindResource("ButtonStyle") };
            deleteButton.Margin = new Thickness(10, 5, 10, 5);
            deleteButton.Name = $"DeleteToy{id}";
            deleteButton.Click += DeleteToy_Click;

            var deleteIcon = new PackIcon() { Style = (Style)FindResource("PackIconStyle") };
            deleteIcon.Kind = PackIconKind.Delete;

            deleteButton.Content = deleteIcon;

            Button editButton = new Button() { Style = (Style)FindResource("ButtonStyle") };
            editButton.Margin = new Thickness(10, 5, 60, 5);
            editButton.Name = $"EditToy{id}";
            editButton.Click += EditToy_Click;

            PackIcon editIcon = new PackIcon() { Style = (Style)FindResource("PackIconStyle") };
            editIcon.Kind = PackIconKind.Edit;

            editButton.Content = editIcon;

            grid.Children.Add(textBlock);
            grid.Children.Add(deleteButton);
            grid.Children.Add(editButton);

            List.Children.Add(grid);
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject {
            T parent = VisualTreeHelper.GetParent(child) as T;

            if (parent != null)
                return parent;
            else
                return FindParent<T>(parent);
        }
    }
}
