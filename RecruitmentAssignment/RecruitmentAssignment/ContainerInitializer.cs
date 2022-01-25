using Autofac;
using RecruitmentAssignment.Authentication;
using RecruitmentAssignment.Services;

namespace RecruitmentAssignment
{
    public static class ContainerInitializer
    {
        public static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<AppSettingsHandler>().As<IAppSettingsHandler>();
        }
    }
}
