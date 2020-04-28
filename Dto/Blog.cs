using System.Collections.Generic;

namespace Dto
{
    public class Blog : DtoBase
    {
        public string Url { get; set; }

        public virtual ICollection<Post> Posts { get; set; } //1=>n relation
    }

}
