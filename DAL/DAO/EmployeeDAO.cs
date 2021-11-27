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

        public static void DeleteEmployee(int employeeId)
        {
            try
            {
                EMPLOYEE borrarUnoSolo = db.EMPLOYEE.First(x => x.ID_Employee == employeeId);
                db.EMPLOYEE.DeleteOnSubmit(borrarUnoSolo);
                db.SubmitChanges();
                //Hay un trigger que realizara las operaciones de borrado de datos , desde la base de datos. 

                ////Borrar tareas asociadas a este empleado
                //List<TASK> listaTask = db.TASK.Where(x => x.ID_Employee == employeeId).ToList();
                //db.TASK.DeleteAllOnSubmit(listaTask);
                //db.SubmitChanges();
                ////Borrar salarios asociados a este empleado
                //List<SALARY> listaSalary = db.SALARY.Where(x => x.ID_Employee == employeeId).ToList();
                //db.SALARY.DeleteAllOnSubmit(listaSalary);
                //db.SubmitChanges();
                ////Borramos los permisos asociados a este empleado
                //List<PERMISSION> listaPermission = db.PERMISSION.Where(x => x.ID_Employee == employeeId).ToList();
                //db.PERMISSION.DeleteAllOnSubmit(listaPermission);
                //db.SubmitChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateEmployee(POSITION position)
        {
            try
            {
                List<EMPLOYEE> employeeList = db.EMPLOYEE.Where(x => x.Position_ID == position.ID).ToList();
                foreach (var item in employeeList)
                {
                    item.Department_ID = (int)position.Deparment_ID;
                }
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }                       
        }

        public static void UpdateEmployee(EMPLOYEE employee)
        {
            try
            {
                EMPLOYEE aux = db.EMPLOYEE.First(x => x.ID_Employee == employee.ID_Employee);
                aux.UserNo = employee.UserNo;
                aux.Name = employee.Name;
                aux.Apellido = employee.Apellido;
                aux.Password = employee.Password;
                aux.isAdmin = employee.isAdmin;
                aux.Birthday = employee.Birthday;
                aux.Adress = employee.Adress;
                aux.Department_ID = employee.Department_ID;
                aux.Position_ID = employee.Position_ID;
                aux.Salary = employee.Salary;
                db.SubmitChanges();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateEmployee(int iD_Employee, int amount)
        {
            try
            {
                EMPLOYEE employee = db.EMPLOYEE.First(x => x.ID_Employee == iD_Employee);
                employee.Salary = amount;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
