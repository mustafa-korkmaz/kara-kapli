using System;
using Microsoft.AspNetCore.Identity;

namespace Dal.Entities.Identity
{
    public class ApplicationUserToken<T> : IdentityUserToken<T> where T : IEquatable<T>
    {
    }
}
