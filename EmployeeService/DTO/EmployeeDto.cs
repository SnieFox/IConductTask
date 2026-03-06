using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ManagerId { get; set; }

        public List<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
    }
}
