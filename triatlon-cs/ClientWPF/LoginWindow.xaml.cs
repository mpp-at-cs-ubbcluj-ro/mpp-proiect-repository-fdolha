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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Clicked(object sender, RoutedEventArgs e)
        {
            TriatlonWindow triatlonWindow = new TriatlonWindow();
            var referee = DependencyProvider.Instance.GetServer().LogInReferee(EmailTextBox.Text, PasswordTextBox.Password, triatlonWindow);
            if (referee != null)
            {
                EmailTextBox.Text = "";
                PasswordTextBox.Password = "";
                triatlonWindow.SetReferee(referee);
                triatlonWindow.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Combinatia email / parola nu exista.", "Eroare Login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
