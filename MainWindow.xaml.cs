//--------------------------------------------------------------------------
// @author:  Nico Solitana, Christian Kevin Villanueva
// @subject: Advanced Operating System
// @course:  MS Computer Science
// @university: De La Salle University - Manila
//--------------------------------------------------------------------------

using SerialSimulation.Controllers;
using SerialSimulation.Models;
using System.Collections.Generic;
using System.Windows;

namespace SerialSimulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TracerData> _tracerData = new List<TracerData>();
        public MainWindow()
        {
            InitializeComponent();
            resultsGrid.Visibility = Visibility.Hidden;
            DataLoading ds = new DataLoading();
            _tracerData = ds.LoadData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string fname = fNameTxt.Text;
            string lname = lNameTxt.Text;
            string simlevel = simLevel.Text;

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
            peopleCount.Content = sp.peopleCount.ToString();
            placesCount.Content = sp.placesCount.ToString();
            daysCount.Content = sp.daysCount.ToString();
            communitiesCount.Content = sp.comCount.ToString();
            resultsGrid.Visibility = Visibility.Visible;
        }


        private void DisplayData(ParallelProcessing pp)
        {
            peopleCount.Content = pp.peopleCount.ToString();
            placesCount.Content = pp.placesCount.ToString();
            daysCount.Content = pp.daysCount.ToString();
            communitiesCount.Content = pp.comCount.ToString();
            resultsGrid.Visibility = Visibility.Visible;
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
