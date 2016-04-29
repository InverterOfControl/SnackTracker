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
        public static void SaveSnack(SnackViewModel snackViewModel) {
            var sessionFactory = SessionFactoryFactory.GetFactory();
            
            using (var session = sessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var snackModel = SnackMap.VmToModel(snackViewModel);
                    session.SaveOrUpdate(snackModel);
                    trans.Commit();
                }
            }
        }


        public static IEnumerable<SnackViewModel> GetSnacks()
        {

            var sessionFactory = SessionFactoryFactory.GetFactory();
            
            using (var session = sessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var snackVMs = session.CreateCriteria<Snack>().List<Snack>();

                    return snackVMs.Select(SnackMap.ModelToVm);
                }
            }
        }
    }
}
