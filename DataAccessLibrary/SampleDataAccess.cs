using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SampleDataAccess
    {
        private readonly IMemoryCache _memoryCache;

        public SampleDataAccess(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> output = new List<Employee>();
            output.Add(new Employee { FirstName = "FN 1", LastName = "LN 1" });
            output.Add(new Employee { FirstName = "FN 2", LastName = "LN 2" });
            output.Add(new Employee { FirstName = "FN 3", LastName = "LN 3" });
            Thread.Sleep(5000);
            return output;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            List<Employee> output = new List<Employee>();
            output.Add(new Employee { FirstName = "FN 1", LastName = "LN 1" });
            output.Add(new Employee { FirstName = "FN 2", LastName = "LN 2" });
            output.Add(new Employee { FirstName = "FN 3", LastName = "LN 3" });
            await Task.Delay(5000);
            return output;
        }

        public async Task<List<Employee>> GetEmployeesCache()
        {
            List<Employee> output;

            output = _memoryCache.Get<List<Employee>>("employees");
            if (output is null)
            {
                output = new();

                output.Add(new Employee { FirstName = "FN 1", LastName = "LN 1" });
                output.Add(new Employee { FirstName = "FN 2", LastName = "LN 2" });
                output.Add(new Employee { FirstName = "FN 3", LastName = "LN 3" });
                await Task.Delay(5000);
                _memoryCache.Set("employees", output, TimeSpan.FromMinutes(1));
            }
            return output;
        }
    }
}
