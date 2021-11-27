using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;
using DAL;

namespace BLL
{
    public class EmployeeBLL
    {
        public static EmployeeDTO GetAll()
        {
            EmployeeDTO dto = new EmployeeDTO();
            dto.Departments = DepartmentDAO.GetDepartments();
            dto.Positions = PositionDAO.GetPositions();
            dto.Employees = EmployeeDAO.GetEmployees();
            return dto;
        }

        public static void addEmployee(EMPLOYEE employee)
        {
            EmployeeDAO.AddEmpployee(employee); 
        }
        //Comprobamos que el numero de usuario sea unico. 
        public static bool isUnique(int v)
        {
            List<EMPLOYEE> list = EmployeeDAO.GetUsers(v);
            if (list.Count > 0)
            {
                return false;
            }
            return true;
        }

        public static List<EMPLOYEE> GetEmployees(int v, string text)
        {
            return EmployeeDAO.GetEmployees(v, text);
        }

        public static void UpdateEmployee(EMPLOYEE employee)
        {
            EmployeeDAO.UpdateEmployee(employee);
        }

        public static void DeleteEmployee(int employeeId)
        {
            EmployeeDAO.DeleteEmployee(employeeId);
        }
    }
}
