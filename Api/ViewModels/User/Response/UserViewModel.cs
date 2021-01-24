using System;
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
        public DateTime MembershipExpiresAt { get; set; }

        public bool EmailConfirmed { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public string Title { get; set; }

        public Settings Settings { get; set; }
    }

    public class Settings
    {
        public string ThemeColor { get; set; }

        public bool OpenTagsView { get; set; }

        public bool FixedHeader { get; set; }

        public string PaginationAlign { get; set; }
    }
}
