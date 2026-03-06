using EmployeeService.DTO;
using EmployeeService.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace EmployeeService.DomainServices
{
    public class EmployeeDomainService
    {
        private readonly EmployeeRepository repository = new EmployeeRepository();

        public List<EmployeeDto> GetEmployees()
        {
            var employees = repository.GetEmployees();

            return BuildHierarchy(employees);
        }

        public void SetEmployeeStatus(int id, bool isEnable)
        {
            repository.UpdateEmployeeEnable(id, isEnable);
        }

        private List<EmployeeDto> BuildHierarchy(List<Employee> employees)
        {
            var dict = employees.ToDictionary(x => x.Id, x => new EmployeeDto
            {
                Id = x.Id,
                Name = x.Name,
                ManagerId = x.ManagerId
            });

            var roots = new List<EmployeeDto>();

            foreach (var employee in dict.Values)
            {
                if (employee.ManagerId == null)
                {
                    roots.Add(employee);
                }
                else
                {
                    if (dict.ContainsKey(employee.ManagerId.Value))
                    {
                        dict[employee.ManagerId.Value].Employees.Add(employee);
                    }
                    else
                    {
                        roots.Add(employee);
                    }
                }
            }

            return roots;
        }
    }
}
