﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem
{
    internal class DBConnection
    {
        public string MyConnection()
        {
            string conn = "Server=localhost;Database=student;User ID=root;Password=;SslMode=None;";
            return conn;
        }
    }
}
