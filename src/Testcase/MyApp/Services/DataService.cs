using System.Collections.Generic;
using MyApp.Interfaces;

namespace MyApp.Services
{
    public class DataService : IDataService
    {
        public IList<string> GetData(string subject)
        {
            // returns data by subject
            return new List<string> { "apple", "orange", "banana" };
        }
    }
}