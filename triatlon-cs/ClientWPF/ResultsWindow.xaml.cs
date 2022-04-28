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
using Model;
using Model.Activity;
using Service;

namespace ClientWPF
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        private RaceType _raceType;
        private readonly ITriatlonService _server = DependencyProvider.Instance.GetServer();
        public ResultsWindow(RaceType raceType)
        {
            _raceType = raceType;
            InitializeComponent();
            ResultsTitle.Content = $"Rezultate {raceType}";
            var results = _server.GetParticipantsWithResultInRace(raceType);
            results.Sort((l, r) => r.Points.CompareTo(l.Points));
            ResultsDataGrid.ItemsSource = results;
        }
    }
}
