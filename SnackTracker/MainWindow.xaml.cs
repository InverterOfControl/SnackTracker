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
        public MainWindow()
        {
            InitializeComponent();
            SetupBindings();
            
            this.DataContext = this;
        }

        private void SetupBindings()
        {
            var viewModel = new SnackListViewModel();
            snackListView.DataContext = viewModel;
        }
    }
}
