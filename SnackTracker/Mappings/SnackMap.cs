using System;
using SnackTracker.ViewModels;
using FluentNHibernate.Mapping;
using SnackTracker.Models;

namespace SnackTracker.Mappings
{
    public class SnackMap : ClassMap<Snack> {

        public SnackMap(){
            Id(x => x.Id);
            Map(x => x.Date);
            Map(x => x.Name);
            Map(x => x.PricePerUnit);
            Map(x => x.Quantity);
        }

        //public static Snack VmToModel(SnackViewModel vm){
        //    return new Snack{ Date = vm.Date, Name = vm.Name, PricePerUnit = vm.PricePerUnit, Quantity = vm.Quantity };
        //}

        //public static SnackViewModel ModelToVm(Snack m){
        //    return new SnackViewModel { Quantity = m.Quantity, PricePerUnit = m.PricePerUnit, Name = m.Name, Date = m.Date };
        //}
    }
}
