using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class SalaryDAO : EmployeeContext
    {
        public static List<MONTH> GetMonths()
        {
            return db.MONTH.ToList();
        }

        public static void AddSalary(SALARY salary)
        {
            try
            {
                db.SALARY.InsertOnSubmit(salary);
                db.SubmitChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SalaryDetailDTO> GetSalaries()
        {
            List<SalaryDetailDTO> salaryList = new List<SalaryDetailDTO>();
            var list = (from s in db.SALARY
                        join e in db.EMPLOYEE on s.ID_Employee equals e.ID_Employee
                        join d in db.DEPARTMENT on e.Department_ID equals d.ID
                        join p in db.POSITION on e.Position_ID equals p.ID
                        join m in db.MONTH on s.Id_Month equals m.Id_Month
                        select new
                        {
                            userNo = e.UserNo,
                            name = e.Name,
                            apellido = e.Apellido,
                            idEmployee = e.ID_Employee,
                            amount = s.Amount,
                            year = s.Year,
                            monthName = m.MonthName,
                            idMonth = m.Id_Month,
                            idSalary = s.ID_Salary,
                            idDeparment = e.Department_ID, 
                            departmentName = d.DepartmentName,
                            idPosition = e.Position_ID,
                            positionName = p.PositionName

                        }).OrderBy(x=>x.year).ToList();
            foreach (var item in list)
            {
                SalaryDetailDTO dto = new SalaryDetailDTO();
                dto.UserNo = item.userNo;
                dto.Name = item.name;
                dto.Surname = item.apellido;
                dto.DepartmentID = item.idDeparment;
                dto.DepartmentName = item.departmentName;
                dto.PositionName = item.positionName;
                dto.EmployeeId = item.idEmployee;
                dto.SalaryAmount = item.amount;
                dto.SalaryYear = item.year;
                dto.MonthID = item.idMonth;
                dto.MonthName = item.monthName;
                dto.SalaryID = item.idSalary;
                dto.PositionID = item.idPosition;
                dto.PositionName = item.positionName;
                dto.OldSalary = item.amount;
                salaryList.Add(dto);
        
    }

            return salaryList;
        }

        public static void DeleteSalary(int salaryID)
        {
            try
            {
                SALARY borrarUnoSolo = db.SALARY.First(x => x.ID_Salary == salaryID);
                db.SALARY.DeleteOnSubmit(borrarUnoSolo);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateSalary(SALARY salary)
        {
            try
            {
                SALARY sl = db.SALARY.First(x => x.ID_Salary == salary.ID_Salary);
                sl.Amount = salary.Amount;
                sl.Year = salary.Year;
                sl.Id_Month = salary.Id_Month;
                db.SubmitChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
