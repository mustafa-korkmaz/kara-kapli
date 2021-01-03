
using System.Threading.Tasks;

namespace Service.Slack
{
    public interface ISlackService
    {
        Task SendMessage(string message, string channel);
    }
}