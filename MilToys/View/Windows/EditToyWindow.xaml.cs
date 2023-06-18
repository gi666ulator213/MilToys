using System;
using System.Windows;
using static MilToys.Utils;

namespace MilToys.View.Windows {
    public partial class EditToyWindow : Window {
        public EditToyWindow() {
            InitializeComponent();
        }

        int CurrentToyPrice;

        private void ToySave_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(ToyName.Text) ||
                string.IsNullOrEmpty(ToyStock.Text) ||
                string.IsNullOrEmpty(ToyAge.Text) ||
                string.IsNullOrEmpty(ToyPrice.Text)) {

                ShowError("Заповніть усі поля");
            }
            else if (!int.TryParse(ToyPrice.Text, out _))
            {
                ShowError("Невірно введені дані");
            }
            else {
                int id = (Owner as EditWindow).EditedToy.Id;
                EditDB.EditToy(id, ToyStock.Text, ToyAge.Text, Convert.ToInt32(ToyPrice.Text));

                if (CurrentToyPrice != Convert.ToInt32(ToyPrice.Text)) {
                    (Owner as EditWindow).EditedToyGrid.Visibility = Visibility.Collapsed;
                }
                
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            CurrentToyPrice = Convert.ToInt32(ToyPrice.Text);
        }
    }
}
