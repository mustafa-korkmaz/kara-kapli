﻿using System;

namespace Api.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string NameSurname { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}
