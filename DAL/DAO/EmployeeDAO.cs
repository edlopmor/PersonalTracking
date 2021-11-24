using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class EmployeeDAO : EmployeeContext
    {
        public static void AddEmpployee(EMPLOYEE employee)
        {
            try
            {
                db.EMPLOYEE.InsertOnSubmit(employee);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<EmployeeDetailDTO> GetEmployees()
        {
            List<EmployeeDetailDTO> employeesList = new List<EmployeeDetailDTO>();
            //Creamos una lista con los datos que vamos a recuperar de la base de datos. 
            var list = (from e in db.EMPLOYEE
                        join d in db.DEPARTMENT on e.Department_ID equals d.ID
                        join p in db.POSITION on e.Position_ID equals p.ID
                        select new
                        {
                            UserNo = e.UserNo,
                            Name = e.Name,
                            Surname = e.Apellido,
                            EmployeeID = e.ID_Employee,
                            password = e.Password,
                            DepartmentName = d.DepartmentName,
                            PositionName = p.PositionName,
                            DepartmendID = e.Department_ID,
                            PositionID = e.Position_ID,
                            isAdmin = e.isAdmin,
                            Salary = e.Salary,
                            ImagePath = e.ImagePath,
                            birthday = e.Birthday,
                            Adress = e.Adress

                        }).OrderBy(x => x.UserNo).ToList();
            foreach (var item in list)
            {
                EmployeeDetailDTO dto = new EmployeeDetailDTO();
                dto.UserNo = item.UserNo;
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.EmployeeId = item.EmployeeID;
                dto.Password = item.password;
                dto.DepartmentName = item.DepartmentName;
                dto.PositionName = item.PositionName;
                dto.DepartmentID = item.DepartmendID;
                dto.PositionID = item.PositionID;
                dto.isAdmin = item.isAdmin;
                dto.Salary = item.Salary;
                dto.ImagePath = item.ImagePath;
                dto.Birthday = item.birthday;
                dto.Adress = item.Adress;

                employeesList.Add(dto);
                
            }
            return employeesList;
        }

        public static List<EMPLOYEE> GetEmployees(int v, string text)
        {
            try
            {
                List<EMPLOYEE> list = db.EMPLOYEE.Where(x => x.UserNo == v && x.Password == text).ToList();
                return list;
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<EMPLOYEE> GetUsers(int v)
        {
            return db.EMPLOYEE.Where(x => x.UserNo == v).ToList();
        }
    }
}
