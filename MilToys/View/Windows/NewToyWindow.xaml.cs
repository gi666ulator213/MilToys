using System;
using System.Windows;
using static MilToys.Utils;

namespace MilToys.View.Windows {
    public partial class NewToyWindow : Window {
        public NewToyWindow() {
            InitializeComponent();
        }

        private void ToyAdd_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(ToyName.Text) ||
                string.IsNullOrEmpty(ToyStock.Text) ||
                string.IsNullOrEmpty(ToyAge.Text)) {

                ShowError("Заповніть усі поля");
            }
            else if (!int.TryParse(ToyPrice.Text, out _))
            {
                ShowError("Невірно введені дані");
            }
            else {
                int id = EditDB.AddToy(new Toy(0, ToyName.Text, ToyStock.Text, ToyAge.Text, Convert.ToInt32(ToyPrice.Text)));
                string name = ToyName.Text;

                var win = (Owner as EditWindow);

                if (win.OpenedAgeLimit == null) {
                    win.Return_Click(null, null);
                }
                else if (win.OpenedAgeLimit == Convert.ToString(ToyPrice.Text)) {
                    win.AddToyToList(name, id);
                }

                Close();
            }

        }
    }
}
