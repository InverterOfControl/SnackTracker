using SnackTracker.Models;
using System;
using System.ComponentModel;
using ViewModelBase;

namespace SnackTracker.ViewModels
{
    public class SnackViewModel : ViewModel<Snack> {

        private String name;
        public String Name { get{return this.name;}
            set{
                this.name = value;
                this.OnPropertyChanged("Name");
            } }

        private double pricePerUnit;
        public double PricePerUnit { get { return this.pricePerUnit; }
            set {
                this.pricePerUnit = value;
                this.OnPropertyChanged("PricePerUnit");
                this.OnPropertyChanged("Sum");
            }
        }
        private int quantity;
        public int Quantity { get { return this.quantity; }
            set {
                this.quantity = value;
                this.OnPropertyChanged("Quantity");
                this.OnPropertyChanged("Sum");
            }
        }

        public double Sum { get { return this.Quantity * this.PricePerUnit; } }

        private DateTime date;
        public DateTime Date { get { return this.date; }
            set {
                this.date = value;
                this.OnPropertyChanged("Date");
            }
        }

        public SnackViewModel(Snack model) : base(model){
            this.Date = DateTime.Now;
        }

        public SnackViewModel(): base()
        {
        }
    }
}
