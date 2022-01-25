using Microsoft.Extensions.Configuration;

namespace RecruitmentAssignment.Authentication
{
    public class AppSettingsHandler : IAppSettingsHandler
    {
        private readonly IConfiguration _configuration;

        public AppSettingsHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetApiKey()
        {
            return _configuration.GetValue<string>("ApiKey");
        }
    }
}
