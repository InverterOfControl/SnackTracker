using SnackTracker.Configuration;
using SnackTracker.Mappings;
using SnackTracker.Models;
using SnackTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackTracker.DB
{
    public static class Persister {
        public static void SaveSnack(Snack snackModel) {
            var sessionFactory = SessionFactoryFactory.GetFactory();
            
            using (var session = sessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.SaveOrUpdate(snackModel);
                    trans.Commit();
                }
            }
        }


        public static IEnumerable<Snack> GetSnacks()
        {
            var sessionFactory = SessionFactoryFactory.GetFactory();
            
            using (var session = sessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    return session.CreateCriteria<Snack>().List<Snack>();
                }
            }
        }

        internal static void RemoveSnack(Snack snack)
        {
            var sessionFactory = SessionFactoryFactory.GetFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.Delete(snack);
                    trans.Commit();
                }
            }
        }

        internal static void ClearSnacks()
        {
            var sessionFactory = SessionFactoryFactory.GetFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.Delete("SELECT * FROM Snacks");
                    trans.Commit();
                }
            }
        }
    }
}
