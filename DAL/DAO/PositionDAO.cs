using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class PositionDAO : EmployeeContext
    {
        public static void AddPosition(POSITION position)
        {
            try
            {
                db.POSITION.InsertOnSubmit(position);
                db.SubmitChanges();
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PositionDTO> GetPositions()
        {
            try
            {
                var list = (from p in db.POSITION
                            join d in db.DEPARTMENT on p.Deparment_ID equals d.ID
                            select new
                            {
                                positionID = p.ID,
                                positionName = p.PositionName,
                                departmentName = d.DepartmentName,
                                departmentID = d.ID
                            }).OrderBy(x => x.positionID).ToList();

                List<PositionDTO> positionList = new List<PositionDTO>();
                    foreach (var item in list)
                    {
                        PositionDTO dto = new PositionDTO();
                        dto.ID = item.positionID;
                        dto.PositionName = item.positionName;
                        dto.Deparment_ID = item.departmentID;
                        dto.DepartmentName = item.departmentName;
                        

                        positionList.Add(dto);
                    }
                return positionList;
            }
                
            catch (Exception ex )
            {

                throw ex;
            }
        }

        public static void PositionDelete(int id)
        {
            try
            {
                POSITION borrarUnoSolo = db.POSITION.First(x => x.ID == id);
                db.POSITION.DeleteOnSubmit(borrarUnoSolo);
                db.SubmitChanges();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void PositionUpdate(POSITION position)
        {
            try
            {
                POSITION aux = db.POSITION.First(x => x.ID == position.ID);
                aux.PositionName = position.PositionName;
                aux.Deparment_ID = position.Deparment_ID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
