using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class PermissionDetailDTO
    {
        public int EmployeeId { get; set; }
        public int UserNo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public int PositionID { get; set; }
        public string PositionName { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int PermissionAmount { get; set; }

        public string StateName { get; set; }
        public int StateId { get; set; }
        public int StatePermission { get; set; }
        public string Motivo { get; set; }
    }
}
