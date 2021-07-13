using System;
using System.Collections.Generic;

namespace Exercise_5_Garage
{
    public interface IHandler
    {
        internal (Garage<IVehicle>, bool) Initiate(int size, int numberOfVehicles);
        internal IVehicle CreateRandomVehicle();
        IEnumerable<Type> TypesOfVehicles();
        internal IVehicle CreateVehicle(List<object> specifics);
        internal void AddVehicle(Garage<IVehicle> garage, IVehicle vehicle, bool v);
        internal void RemoveVehicle(Garage<IVehicle> garage, string v1);
        internal int NumberOf(Garage<IVehicle> garage, Type type);
        internal string GetRegNummer(IVehicle item);
        internal (int, bool, string) RegNumberSearch(Garage<IVehicle> garage, string regNum);
        internal List<IVehicle> CharacteristicsSearch(Garage<IVehicle> garage, List<string> searchPropList, List<string> searchTermList);
    }
}