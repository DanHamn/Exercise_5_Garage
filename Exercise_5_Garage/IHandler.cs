using System;
using System.Collections.Generic;

namespace Exercise_5_Garage
{
    public interface IHandler
    {
        internal (Garage<Vehicle>, bool) Initiate(int size, int numberOfVehicles);
        Vehicle CreateRandomVehicle();
        IEnumerable<Type> TypesOfVehicles();
        internal Vehicle CreateVehicle(List<object> specifics);
        internal void RemoveVehicle(Garage<Vehicle> garage, string v1);
        internal int NumberOf(Garage<Vehicle> garage, Type type);
        internal string GetRegNummer(Vehicle item);
        internal (int, bool, string) RegNumberSearch(Garage<Vehicle> garage, string regNum);
        internal int CharacteristicsSearch(Garage<Vehicle> garage, string searchProp, string searchTerm);
    }
}