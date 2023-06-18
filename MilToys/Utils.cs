using System.Windows;

namespace MilToys {
    class Utils {
        public static void ShowError(string message, string caption = "Помилка") {

            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
