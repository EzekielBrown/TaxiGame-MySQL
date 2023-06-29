using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace TaxiGame
{
    public class PlayerInDB
    {
        private string _username;
        private string _password;
        private string _email;
        private bool _isAdmin;
        private bool _isLocked;
        private int _numLoginAttempts;
        private bool _isOnline;
        private int _score;

        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public string Email { get => _email; set => _email = value; }
        public bool IsAdmin { get => _isAdmin; set => _isAdmin = value; }
        public bool IsLocked { get => _isLocked; set => _isLocked = value; }
        public int NumLoginAttempts { get => _numLoginAttempts; set => _numLoginAttempts = value; }
        public bool IsOnline { get => _isOnline; set => _isOnline = value; }
        public int Score { get => _score; set => _score = value; }
    }
}
