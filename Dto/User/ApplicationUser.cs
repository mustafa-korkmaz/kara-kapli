using System;
using System.Collections.Generic;

namespace Dto.User
{
    public class ApplicationUser
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string NameSurname { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string ImageName { get; set; }

        public string PhoneNumber { get; set; }

        public bool ContactPermission { get; set; }

        public DateTime CreatedAt { get; set; }

        public IList<string> Roles { get; set; }

        public Dictionary<string, string> Claims { get; set; }

        //public bool IsAdmin
        //{
        //    get
        //    {
        //        if (Roles == null)
        //        {
        //            return false;
        //        }
        //        return Roles.Any(r => r == Role.Admin);
        //    }
        //}
    }
}
