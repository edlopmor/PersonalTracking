using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class DepartmentDAO : EmployeeContext
    {
        public static void AddDepartment(DEPARTMENT department)
        {
            try
            {
                db.DEPARTMENT.InsertOnSubmit(department);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
                
            
        }

        public static List<DEPARTMENT> GetDepartments()
        {
            return db.DEPARTMENT.ToList();
        }

        public static void UpdateDepartment(DEPARTMENT department)
        {
            try
            {
                DEPARTMENT aux = db.DEPARTMENT.First(x => x.ID == department.ID);
                aux.DepartmentName = department.DepartmentName;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void DeleteDepartment(int iD)
        {
            try
            {
                DEPARTMENT borrarUnosolo = db.DEPARTMENT.First(x => x.ID == iD);
                db.DEPARTMENT.DeleteOnSubmit(borrarUnosolo);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
