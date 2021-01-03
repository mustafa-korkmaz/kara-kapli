using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Service.Slack
{
    public class SlackService : ISlackService
    {
        private readonly ILogger<SlackService> _logger;
        private readonly IOptions<AppSettings> _appSettings;

        public SlackService(IOptions<AppSettings> appSettings, ILogger<SlackService> logger)
        {
            _appSettings = appSettings;
            _logger = logger;

        }

        public async Task SendMessage(string message, string channel)
        {
            var url = "https://slack.com/api/chat.postMessage";
            var token = _appSettings.Value.SlackBotToken;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", token);

                var payload = new
                {
                    text = message,
                    channel
                };

                var json = JsonConvert.SerializeObject(payload);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, data);
            }
        }
    }
}