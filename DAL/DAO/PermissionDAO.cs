﻿using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class PermissionDAO : EmployeeContext
    {
        public static void AddPermision(PERMISSION permission)
        {
            try
            {
                db.PERMISSION.InsertOnSubmit(permission);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<PERMISSIONSTATE> GetStates()
        {
            return db.PERMISSIONSTATE.ToList();
        }

        public static List<PermissionDetailDTO> GetPermissions()
        {
            List<PermissionDetailDTO> permissions = new List<PermissionDetailDTO>();
            var list = (from p in db.PERMISSION
                        join s in db.PERMISSIONSTATE on p.PermissionState equals s.ID_PermissionState
                        join e in db.EMPLOYEE on p.ID_Employee equals e.ID_Employee
                        select new
                        {
                            userNo = e.UserNo,
                            name = e.Name,
                            surName = e.Apellido,
                            stateName = s.StateName,
                            stateID = s.ID_PermissionState,
                            startDate = p.PermissionStartDate,
                            finishDate = p.PermissionEndDate,
                            employeeId = p.ID_Employee,
                            permissionId = p.ID_Permission,
                            amount = p.PermissionDay,
                            motivo = p.PermissionExplanation,
                            departmentId = e.Department_ID,
                            positionId = e.Position_ID
                        }).OrderBy(x => x.startDate).ToList();
            foreach (var item in list)
            {
                PermissionDetailDTO dto = new PermissionDetailDTO();
                dto.UserNo = item.userNo;
                dto.Name = item.name;
                dto.Surname = item.surName;
                dto.EmployeeId = item.employeeId;
                dto.PermissionAmount= item.amount;
                
                dto.StartDate = item.startDate;
                dto.EndDate = item.finishDate;
                                              
                dto.DepartmentID = item.departmentId;
                dto.PositionID = item.positionId;

                dto.StateId = item.stateID;
                dto.StateName = item.stateName;
                dto.Motivo = item.motivo;

                permissions.Add(dto);
                
            }

            return permissions;
        }
    }
}
