namespace Api.ViewModels.User.Response
{
    public class TokenViewModel
    {
        public string AccessToken { get; set; }

        public string ExpiresIn { get; set; }

        public string NameSurname { get; set; }

        public string Username { get; set; }

        public string Id { get; set; }

        public string Email { get; set; }

    }
}
