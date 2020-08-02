
using System;
using Common.Response;
using Dto.User;

namespace Business.User
{
    public interface IUserBusiness
    {
        Response CreateDemoUserDefaultEntries(Guid userId, string lang);

        Response CreateUserDefaultEntries(Guid userId, string lang);

        UserSettings GetSettings(string settingsJson);

        string GetDefaultSettings();

        Response UpdateSettings(Guid userId, UserSettings newSettings);

        Response UpdateCompanyInformation(Guid userId, string title, string authorizedPerson);
    }
}
