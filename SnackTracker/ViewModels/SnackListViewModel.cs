using SnackTracker.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using System.Collections.Specialized;
using System.Windows.Input;
using SnackTracker.Models;
using System.Windows;

namespace SnackTracker.ViewModels
{
    public class SnackListViewModel : ViewModel {
        
        private ObservableCollection<SnackViewModel> snacks;

        public SnackViewModel NewSnack { get; set; }

        private ICommand addSnackCommand;

        public ObservableCollection<SnackViewModel> Snacks
        {
            get { return snacks; }
            set {
                if (snacks != value)
                {
                    snacks = value;
                    this.OnPropertyChanged("Snacks");
                }
            }
        }

        public SnackListViewModel()
        {
            var snackModels = Persister.GetSnacks();
            snacks = new ObservableCollection<SnackViewModel>(snackModels.Select(snack => new SnackViewModel(snack)));
            snacks.CollectionChanged += Snacks_CollectionChanged;
        }

        private void Snacks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (SnackViewModel svm in e.NewItems)
                    {
                        Persister.SaveSnack(svm.Model);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (SnackViewModel svm in e.OldItems)
                    {
                        Persister.RemoveSnack(svm.Model);
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                        Persister.ClearSnacks();
                    break;
            }
        }

        public ICommand AddSnackCommand
        {
            get {
                if (addSnackCommand == null)
                {
                    addSnackCommand = new RelayCommand(snack => ExecuteAddSnackCommand(snack));
                }

                return addSnackCommand;
            }
        }

        private void ExecuteAddSnackCommand(object snack)
        {
            var newsnack = new Snack { Name = "Snickers", Quantity = 1, PricePerUnit = .8 };

            this.snacks.Add(new SnackViewModel(newsnack));

            //if (string.IsNullOrWhiteSpace(this. snackNameInput.Text) || string.IsNullOrWhiteSpace(snackPriceInput.Text) || string.IsNullOrWhiteSpace(snackQuantityInput.Text))
            //{
            //    MessageBox.Show("Bitte alle Werte angeben");
            //    return;
            //}

            //// check if values correct
            //double snackPrice = 0;
            //int snackQuantity = 0;

            //if (!double.TryParse(snackPriceInput.Text, out snackPrice))
            //{
            //    MessageBox.Show("Snackpreis ist ungültig");
            //    return;
            //}

            //if (!int.TryParse(snackQuantityInput.Text, out snackQuantity))
            //{
            //    MessageBox.Show("Anzahl ist ungültig");
            //    return;
            //}

            //// add
            //var newSnack = new SnackViewModel { Name = snackNameInput.Text, Quantity = snackQuantity, PricePerUnit = snackPrice };

            //this.Snacks.Add(newSnack);

            //Persister.SaveSnack(newSnack);
        }
    }
}
