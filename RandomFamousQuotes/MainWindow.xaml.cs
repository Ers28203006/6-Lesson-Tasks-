using Newtonsoft.Json;
using RandomFamousQuotes.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RandomFamousQuotes
{
    public partial class MainWindow : Window
    {
        List<Country> Countries { get; set; }
        Country Country { get; set; }
        string CountryName { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        void HttpRequest(ComboBox countryComboBox)
        {
            HttpWebRequest httpWebRequest;
            HttpWebResponse httpWebResponse;
            string url = "https://restcountries-v1.p.rapidapi.com/all";
            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Headers["X-RapidAPI-Key"] = "53a4015c41msh796a16c4d9596abp117ce2jsn8668c41603fb";
            httpWebRequest.Headers["X-RapidAPI-Host"] = "restcountries-v1.p.rapidapi.com";
            httpWebRequest.Method = "GET";

            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            Countries = JsonConvert.DeserializeObject<List<Country>>(response);
            foreach (var country in Countries)
                countryComboBox.Items.Add(country.Name);
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() => HttpRequest(countryComboBox));
            });
        }

        void CountryInfo()
        {
            selectGrid.Visibility = Visibility.Collapsed;
            countryInfoGrid.Visibility = Visibility.Visible;

            countryName.Text = "Name: " + Country.Name;
            nativeName.Text = "Native name: " + Country.NativeName;
            capital.Text = "Capital: " + Country.Capital;
            population.Text = "Population: " + Country.Population;
            timezones.Text = "Timezones: " + Country.Timezones[0];
            region.Text = "Region: " + Country.Region;
            subregion.Text = "Subregion: " + Country.Subregion;
            area.Text = "Area: " + Country.Area;
            gini.Text = "Gini: " + Country.Gini;
            numericCode.Text = "Code: " + Country.NumericCode;
            currencies.Text = "Currencies: " + Country.Currencies[0];
        }
        private void CountryComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountryName = countryComboBox.SelectedItem.ToString();
            foreach (var country in Countries)
                if (CountryName == country.Name)
                {
                    Country = country;
                    break;
                }
            CountryInfo();
        }

        int Count { get; set; } = 0;
        private void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (var country in Countries)
            {
                if (searchTextBox.Text == country.Name)
                {
                    Country = country;
                    Count++;
                    break;
                }
            }

            if (Count == 0)
            {
                MessageBox.Show("Incorrect name of the country");
                searchTextBox.Text = "";
            }

            else CountryInfo();
        }
    }
}
