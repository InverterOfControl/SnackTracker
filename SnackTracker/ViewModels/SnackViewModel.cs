using System;
using System.ComponentModel;

namespace SnackTracker.ViewModels
{
    public class SnackViewModel : INotifyPropertyChanged {
        private String name;
        public String Name { get{return this.name;}
            set{
                this.name = value;
                NotifyPropertyChanged("Name");
            } }

        private double pricePerUnit;
        public double PricePerUnit { get { return this.pricePerUnit; }
            set {
                this.pricePerUnit = value;
                NotifyPropertyChanged("PricePerUnit");
                NotifyPropertyChanged("Sum");
            }
        }
        private int quantity;
        public int Quantity { get { return this.quantity; }
            set {
                this.quantity = value;
                NotifyPropertyChanged("Quantity");
                NotifyPropertyChanged("Sum");
            }
        }

        public double Sum { get { return this.Quantity * this.PricePerUnit; } }

        private DateTime date;
        public DateTime Date { get { return this.date; }
            set {
                this.date = value;
                NotifyPropertyChanged("Date");
            }
        }

        public SnackViewModel(){
            this.Date = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
