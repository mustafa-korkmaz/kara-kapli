using System;
using Microsoft.AspNetCore.Identity;

namespace Dal.Entities.Identity
{
    public class ApplicationUserClaim<T> : IdentityUserClaim<T> where T : IEquatable<T>
    {
    }
}
