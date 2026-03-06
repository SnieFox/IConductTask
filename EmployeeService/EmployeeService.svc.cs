using EmployeeService.DomainServices;
using EmployeeService.DTO;
using EmployeeService.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;

namespace EmployeeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDomainService _domainService = new EmployeeDomainService();

        public List<EmployeeDto> GetEmployees()
        {
            try
            {
                return _domainService.GetEmployees();
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>("Ошибка при получении списка сотрудников: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void EnableEmployee(int id, bool enable)
        {
            try
            {
                _domainService.SetEmployeeStatus(id, enable);
            }
            catch (ArgumentException ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>("Не удалось обновить статус сотрудника.", HttpStatusCode.InternalServerError);
            }
        }

        public bool GetEmployeeById(int id)
        {
            return false;
        }
    }
}