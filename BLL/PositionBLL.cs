﻿using DAL;
using DAL.DAO;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PositionBLL
    {
        public static void AddPosition(POSITION position)
        {
            
            PositionDAO.AddPosition(position);
        }

        public static List<PositionDTO> GetPositions()
        {
            return PositionDAO.GetPositions();
        }

        public static void UpdatePosition(POSITION position, bool control)
        {
            PositionDAO.PositionUpdate(position);
            if (control) ;
            EmployeeDAO.UpdateEmployee(position);
        }

        public static void DeletePosition(int id)
        {
            PositionDAO.PositionDelete(id);
        }
    }
}
