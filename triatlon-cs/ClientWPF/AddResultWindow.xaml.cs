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
using Service;
using Model;
using Model.Person;
using System.Text.RegularExpressions;

namespace ClientWPF
{
    /// <summary>
    /// Interaction logic for AddResultWindow.xaml
    /// </summary>
    public partial class AddResultWindow : Window
    {
        private readonly ITriatlonService _server = DependencyProvider.Instance.GetServer();
        private List<String> _athletesNames = new();
        private Referee _referee;

        public AddResultWindow(Referee referee)
        {
            _referee = referee;
            InitializeComponent();
            _server.GetAthletes().ForEach(a => _athletesNames.Add($"{a.Id} - {a.FullName}"));
            AthletesComboBox.ItemsSource = _athletesNames;
        }

        private void NumberValidationForPointsTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CancelButton_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (AthletesComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Te rugam sa alegi un participant pentru a introduce un rezultat.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(PointsTextBox.Text))
            {
                MessageBox.Show("Te rugam sa introduci un numar de puncte.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _server.AddResult(_referee, AthletesComboBox.SelectedIndex + 1, int.Parse(PointsTextBox.Text));
            Close();
        }
    }
}
