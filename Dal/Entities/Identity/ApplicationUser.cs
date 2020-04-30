﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Dal.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [MaxLength(50)]
        public string NameSurname { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Parameter> Parameters { get; set; }// 1=>n relation
    }
}