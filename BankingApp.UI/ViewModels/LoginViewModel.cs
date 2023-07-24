﻿using BankingApp.BLL;
using BankingApp.UI.Commands;
using BankingApp.UI.NavigationServices;
using System;
using System.Windows;

namespace BankingApp.UI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly UserService _userService;
        private readonly CustomerService _customerService;
        private readonly INavigationService _navigationService;
        private string _username;
        private string _password;

        public RelayCommand LoginCommand { get; private set; }

        public Action ClearPasswordAction { get; set; }

        public Action CloseAction { get; set; }

        public LoginViewModel(UserService userService, CustomerService customerService, INavigationService navigationService)
        {
            _userService = userService;
            _customerService = customerService;
            _navigationService = navigationService;
            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            private get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        

        private bool CanLogin(object obj)
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }
        private void Login(object obj)
        {
            var user = _userService.FetchUser(Username, Password);
            if (user == null)
            {
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                Username = "";
                ClearPasswordAction?.Invoke(); // Invoke the action to clear the PasswordBox

            }
            else
            {
                MainViewModel mainViewModel = new MainViewModel(_userService, _customerService, _navigationService);
                _navigationService.Navigate(mainViewModel);
                CloseAction?.Invoke(); // Close the LoginWindow
            }
        }
    }
}
