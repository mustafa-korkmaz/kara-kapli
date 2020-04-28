using Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.ViewModels
{
    public class SnakeCaseQueryAttribute : FromQueryAttribute
    {
        public SnakeCaseQueryAttribute(string name)
        {
            Name = name.ToSnakeCase();
        }
    }

    public class SnakeCaseRouteAttribute : FromRouteAttribute
    {
        public SnakeCaseRouteAttribute(string name)
        {
            Name = name.ToSnakeCase();
        }
    }
}
