using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using SnackTracker.ViewModels;
using SnackTracker.Configuration;
using SnackTracker.Mappings;
using SnackTracker.Models;
using SnackTracker.DB;

namespace SnackTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public ObservableCollection<SnackViewModel> Snacks { get; set; }

        public double OverallSum { get { return this.Snacks.Sum(x => x.Sum); } }

        private void AddSnackCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e){
            e.CanExecute = true;
        }

        private void AddSnackCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           // check if inputs filled
            if (string.IsNullOrWhiteSpace(snackNameInput.Text) || string.IsNullOrWhiteSpace(snackPriceInput.Text) || string.IsNullOrWhiteSpace(snackQuantityInput.Text)) {
                MessageBox.Show("Bitte alle Werte angeben");
                return;
            }

            // check if values correct
            double snackPrice = 0;
            int snackQuantity = 0;

            if (!double.TryParse(snackPriceInput.Text, out snackPrice)) {
                MessageBox.Show("Snackpreis ist ungültig");
                return;
            }

            if (!int.TryParse(snackQuantityInput.Text, out snackQuantity)) {
                MessageBox.Show("Anzahl ist ungültig");
                return;
            }

            // add
            var newSnack = new SnackViewModel { Name = snackNameInput.Text, Quantity = snackQuantity, PricePerUnit = snackPrice };
            
            this.Snacks.Add(newSnack);

            Persister.SaveSnack(newSnack);

            // avoid INotifyProperty-Madness
            this.OverAllSumTextBlock.Text = this.OverallSum.ToString("#,##0.00 €;(#,##0.00) €");
        }

        public MainWindow()
        {
            InitializeComponent();

            this.Snacks = new ObservableCollection<SnackViewModel>(Persister.GetSnacks());

            this.DataContext = this;
        }
    }
}
