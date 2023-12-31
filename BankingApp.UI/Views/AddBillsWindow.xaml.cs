﻿using System.Windows;
using BankingApp.UI.ViewModels;
namespace BankingApp.UI.Views
{
    public partial class AddBillsWindow : Window
    {
        public AddBillsWindow(AddBillsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.CloseAction = Close;
        }
    }
}
