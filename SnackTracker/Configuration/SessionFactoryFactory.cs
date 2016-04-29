using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace SnackTracker.Configuration
{
    public static class SessionFactoryFactory
    {
        public static ISessionFactory GetFactory()
        {
            var sessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile("snacks.db"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<App>())
                .ExposeConfiguration(SchemaBuilder.BuildSchema)
                .BuildSessionFactory();

            return sessionFactory;
        }
    }
}
