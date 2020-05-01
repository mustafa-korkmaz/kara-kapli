using Dto.User;
using System;

namespace Dto
{
    public class Parameter : DtoBase
    {
        /// <summary>
        /// parameterTypes.Id FK
        /// </summary>
        public int ParameterTypeId { get; set; }
        public virtual ParameterType ParameterType { get; set; } //navigation

        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } // navigation 

        public string Name { get; set; }

        public byte Order { get; set; }

        public bool IsDeleted { get; set; }
    }
}
