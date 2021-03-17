using System;
using System.Collections.Generic;
using RPGCharacterCreatorClient.Data;
using RPGCharacterCreatorClient.Views;

namespace TenmoClient
{
    class Program
    {

        static void Main(string[] args)
        {
            AuthService authService = new AuthService();
            new LoginRegisterMenu(authService).Show();

        }
    }
}
