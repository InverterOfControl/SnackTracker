using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace ViewModelBase
{
    [Serializable]
    public abstract class ViewModel : IViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        /// <remarks></remarks>
        protected ViewModel()
        {
            var initializationTask = new Task(() => Initialize());
            initializationTask.ContinueWith(result => InitializationCompletedCallback(result));
            initializationTask.Start();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected virtual void Initialize()
        {
        }

        /// <summary>
        /// Callback method for the async initialization.
        /// </summary>
        /// <param name="result">The result.</param>
        private void InitializationCompletedCallback(IAsyncResult result)
        {
            var initializationCompleted = InitializationCompleted;
            if (initializationCompleted != null)
            {
                InitializationCompleted(this, new AsyncCompletedEventArgs(null, !result.IsCompleted, result.AsyncState));
            }
            InitializationCompleted = null;
        }

        /// <summary>
        /// Occurs when the initialization is completed.
        /// </summary>
        public event AsyncCompletedEventHandler InitializationCompleted;

        /// <summary>
        /// Called when a property has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <remarks></remarks>
        protected virtual void OnPropertyChanged(string propertyName)
        {

            var evt = PropertyChanged;
            if (evt != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        /// <remarks></remarks>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    /// <summary>
    /// The view Model base class.
    /// </summary>
    public abstract class ViewModel<TModel> : ViewModel, IViewModel<TModel> where TModel : class, new()
    {
        private TModel model;

        /// <summary>
        /// The Model encapsulated by this ViewModel.
        /// </summary>
        /// <remarks>If you change this, all needed PropertyChanged events will be raised automatically.</remarks>
        [Browsable(false)]
        [Bindable(false)]
        public TModel Model
        {
            get
            {
                return model;
            }
            set
            {
                if (Model != value)
                {
                    // get all properties
                    var properties = this.GetType().GetProperties(BindingFlags.Public);
                    // all values before the model has changed
                    var oldValues = properties.Select(p => p.GetValue(this, null));
                    var enumerator = oldValues.GetEnumerator();

                    model = value;

                    // call OnPropertyChanged for all changed properties
                    foreach (var property in properties)
                    {
                        enumerator.MoveNext();
                        var oldValue = enumerator.Current;
                        var newValue = property.GetValue(this, null);

                        if ((oldValue == null && newValue != null)
                            || (oldValue != null && newValue == null)
                            || (!oldValue.Equals(newValue)))
                        {
                            OnPropertyChanged(property.Name);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class.
        /// </summary>
        /// <remarks></remarks>
        protected ViewModel(TModel model)
            : base()
        {
            this.Model = model;
        }

        public ViewModel(): base()
        {
            this.Model = new TModel();
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Model.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as IViewModel<TModel>;

            if (other == null)
                return false;

            return Equals(other);
        }


        /// <summary>
        /// Determines whether the specified <see cref="IViewModel<TModel>"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="IViewModel<TModel>"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="IViewModel<TModel>"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(IViewModel<TModel> other)
        {
            if (other == null)
                return false;

            if (Model == null)
                return Model == other.Model;

            return Model.Equals(other.Model);
        }
    }
}
