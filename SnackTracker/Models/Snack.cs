using System;

namespace SnackTracker.Models
{
    public class Snack
    {
        public virtual int Id { get; set; }

        public virtual String Name { get; set; }

        public virtual double PricePerUnit { get; set; }

        public virtual int Quantity { get; set; }

        public virtual DateTime Date { get; set; }
    }
}
