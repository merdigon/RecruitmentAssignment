using Autofac;
using RecruitmentAssignment.DAL.Repositories;
using RecruitmentAssignment.DAL.SqlLite.Connection;
using RecruitmentAssignment.DAL.SqlLite.Repositories;

namespace RecruitmentAssignment.DAL.SqlLite
{
    public static class ContainerInitializer
    {
        public static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            builder.RegisterType<SqlLiteConnectionFactory>().As<ISqlLiteConnectionFactory>().SingleInstance();
        }
    }
}
