using System;

namespace Dto
{
    public class Post : DtoBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; } // navigation 
    }
}
