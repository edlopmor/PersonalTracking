using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class PermissionDTO
    {
        
        public List<PERMISSIONSTATE> States { get; set; }
        public List<PermissionDetailDTO> Permissions { get; set; }

        public List<DEPARTMENT> Deparments { get; set; }
        public List<PositionDTO> Positions { get; set; }

        public static List<PermissionDetailDTO> GetPermissions()
        {
            throw new NotImplementedException();
        }
    }
}
