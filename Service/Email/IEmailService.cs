
namespace Service.Email
{
    public interface IEmailService
    {
        bool SendEmail(Email email);
    }
}