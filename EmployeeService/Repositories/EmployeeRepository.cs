using EmployeeService.Entity;
using EmployeeService.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

public class EmployeeRepository : BaseRepository
{
    public List<Employee> GetEmployees()
    {
        var list = new List<Employee>();

        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();

            var command = new SqlCommand(
                "SELECT ID, Name, ManagerID, Enable FROM Employees WHERE Enable = 1",
                connection);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new Employee
                    {
                        Id = (int)reader["ID"],
                        Name = reader["Name"].ToString(),
                        ManagerId = reader["ManagerID"] as int?,
                        Enable = (bool)reader["Enable"]
                    });
                }
            }
        }

        return list;
    }

    public void UpdateEmployeeEnable(int employeeID, bool isEnable)
    {
        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            var command = new SqlCommand(
                "UPDATE Employees SET Enable = @enable WHERE ID = @id",
                connection);

            command.Parameters.AddWithValue("@id", employeeID);
            command.Parameters.AddWithValue("@enable", isEnable);
            command.ExecuteNonQuery();
        }
    }
}