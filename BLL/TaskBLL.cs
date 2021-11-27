using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using DAL;
using DAL.DAO;
using DAL.DTO;

namespace BLL
{
    public class TaskBLL
    {
        public static void Addtask(TASK task)
        {
            /*PositionDAO.AddPosition(position);*/
            TaskDAO.AddTask(task);
        }

        public static TaskDTO GetAll()
        {
            TaskDTO taskDTO = new TaskDTO();
            taskDTO.Employees = EmployeeDAO.GetEmployees();
            taskDTO.Departments = DepartmentDAO.GetDepartments();
            taskDTO.Positions = PositionDAO.GetPositions();

            taskDTO.Taskstates = TaskDAO.GetTaskStates();
            taskDTO.Tasks = TaskDAO.GetTask();

            return taskDTO;
        }

        public static void UpdateTask(TASK update)
        {
            TaskDAO.UpdateTask(update);
        }

        public static void DeleteTask(int taskID)
        {
            TaskDAO.DeleteTask(taskID);
        }
    }
}
