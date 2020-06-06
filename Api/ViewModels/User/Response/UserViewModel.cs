using System;
using System.Collections;
using System.Collections.Generic;

namespace Api.ViewModels.User.Response
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string NameSurname { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool EmailConfirmed { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
