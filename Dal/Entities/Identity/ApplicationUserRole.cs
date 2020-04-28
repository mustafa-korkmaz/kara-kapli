using System;
using Microsoft.AspNetCore.Identity;

namespace Dal.Entities.Identity
{
    public class ApplicationUserRole<T> : IdentityUserRole<T> where T : IEquatable<T>
    {
    }
}
