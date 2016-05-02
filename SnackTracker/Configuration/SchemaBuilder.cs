using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace SnackTracker.Configuration
{
    public static class SchemaBuilder
    {

        public static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
             //delete the existing db on each run
            if (File.Exists("snack.db"))
            {
                File.Delete("snack.db");
            }

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config).Create(false, true);
        }
    }
}
