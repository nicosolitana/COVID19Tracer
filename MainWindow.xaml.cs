//--------------------------------------------------------------------------
// @author:  Nico Solitana, Christian Kevin Villanueva
// @subject: Advanced Operating System
// @course:  MS Computer Science
// @university: De La Salle University - Manila
//--------------------------------------------------------------------------

using SerialSimulation.Controllers;
using SerialSimulation.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SerialSimulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TracerData> _tracerData = new List<TracerData>();
        private string fname;
        private string lname;
        private string simlevel;
        public MainWindow()
        {
            InitializeComponent();
            DataLoading ds = new DataLoading();
            _tracerData = ds.LoadData();
            resultsGrid.Visibility = Visibility.Hidden;
            progressGrid.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            fname = fNameTxt.Text;
            lname = lNameTxt.Text;
            simlevel = simLevel.Text;
            Task.Run(() => StartTracing());
            progressGrid.Visibility = Visibility.Visible;
            resultsGrid.Visibility = Visibility.Hidden;
        }

        private void StartTracing()
        {
            if (simlevel == "Serial")
            {
                SerialProcessing sp = new SerialProcessing();
                sp.StartDataProcessing(fname, lname, _tracerData);
                DisplayData(sp);
            }
            else
            {
                ParallelProcessing pp = new ParallelProcessing();
                pp.StartDataProcessing(fname, lname, _tracerData);
                DisplayData(pp);
            }
        }

        private void DisplayData(SerialProcessing sp)
        {
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                peopleCountSecLevel.Content = sp.secondLevelCount.ToString();
                peopleCount.Content = sp.peopleCount.ToString();
                placesCount.Content = sp.placesCount.ToString();
                daysCount.Content = sp.daysCount.ToString();
                communitiesCount.Content = sp.comCount.ToString();
                resultsGrid.Visibility = Visibility.Visible;
                progressGrid.Visibility = Visibility.Hidden;
            }));
        }


        private void DisplayData(ParallelProcessing pp)
        {
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                peopleCountSecLevel.Content = pp.secondLevelCount.ToString();
                peopleCount.Content = pp.peopleCount.ToString();
                placesCount.Content = pp.placesCount.ToString();
                daysCount.Content = pp.daysCount.ToString();
                communitiesCount.Content = pp.comCount.ToString();
                resultsGrid.Visibility = Visibility.Visible;
                progressGrid.Visibility = Visibility.Hidden;
            }));
        }
    }

    class PersonNameComparer : IEqualityComparer<Person>
    {

        public bool Equals(Person x, Person y)
        {
            return x.firstName == y.firstName && x.lastName == y.lastName;
        }

        public int GetHashCode(Person obj)
        {
            return (obj.firstName == null ? 0 : obj.firstName.GetHashCode()) ^ (obj.lastName == null ? 0 : obj.lastName.GetHashCode());
        }
    }
}
