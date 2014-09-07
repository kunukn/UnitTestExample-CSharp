using System.Collections.Generic;

namespace MyApp.Interfaces
{
    public interface IDataService
    {
        IList<string> GetData(string subject);
    }
}