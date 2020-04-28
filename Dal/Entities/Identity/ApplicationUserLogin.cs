using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Dal.Entities.Identity
{
    public class ApplicationUserLogin<T> : IdentityUserLogin<T> where T : IEquatable<T>
    {
        [Required]
        public DateTime ExpiresAt { get; set; }
    }
}
