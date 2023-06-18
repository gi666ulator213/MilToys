using System.Windows;
using System.Windows.Media;
using static MilToys.Utils;

namespace MilToys.View.Windows {
    public partial class AuthWindow : Window {
        public AuthWindow() {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e) {
            if ((Owner as MainWindow).Login_Click(Login.Text, Password.Password)) {
                Close();
            }
            else {
                Login.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                Password.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                ShowError("Невірний логін або пароль!", "Помилка авторизації");
            }
        }
    }
}
