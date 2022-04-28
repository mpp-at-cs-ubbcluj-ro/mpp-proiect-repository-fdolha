using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using Model.Person;
using Service;

namespace ClientWPF
{
    /// <summary>
    /// Interaction logic for TriatlonWindow.xaml
    /// </summary>
    public partial class TriatlonWindow : Window, ITriatlonObserver
    {
        private Referee? _referee;
        private readonly ITriatlonService _server = DependencyProvider.Instance.GetServer();
        private List<Result> _results = new();
        
        public TriatlonWindow()
        {
            InitializeComponent();
            LoadResults(_server.GetAthletesWithTotalPoints());
        }

        public void SetReferee(Referee referee)
        {
            _referee = referee;
            RefereeName.Content = referee.FullName;
            RefereeRaceType.Content = $"Arbitru {referee.RaceType}";
        }

        private void LoadResults(List<Result> results)
        {
            results.Sort((l, r) => l.Id.CompareTo(r.Id));
            _results = results;
            ResultsDataGrid.ItemsSource = _results;
        }

        void ITriatlonObserver.ResultAdded(List<Result> results)
        {
            Application.Current.Dispatcher.Invoke(new Action(() => LoadResults(results)));
        }

        private void LogOutButton_Clicked(object sender, RoutedEventArgs e)
        {
            DependencyProvider.Instance.GetServer().LogOutReferee(_referee!.Email);
            var loginWindow = new MainWindow();
            loginWindow.Show();
            Close();
        }

        private void AddResultButton_Clicked(object sender, RoutedEventArgs e)
        {
            var addResultWindow = new AddResultWindow(_referee!);
            addResultWindow.Show();
        }

        private void LeaderboardButton_Clicked(object sender, RoutedEventArgs e)
        {
            var resultsWindow = new ResultsWindow(_referee!.RaceType);
            resultsWindow.Show();
        }
    }
}
