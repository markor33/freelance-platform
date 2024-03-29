﻿using Newtonsoft.Json;

namespace Web.Bff.Models
{
    public class LoginRequest
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public LoginRequest() { }

        [JsonConstructor]
        public LoginRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }

    }
}
