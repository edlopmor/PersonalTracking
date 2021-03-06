using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DAL.DAO
{
    public class TaskDAO : EmployeeContext
    {
        public static void AddTask(TASK task)
        {
            try
            {
                db.TASK.InsertOnSubmit(task);
                db.SubmitChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<TASKSTATE> GetTaskStates()
        {
            return db.TASKSTATE.ToList();
        }

        public static List<TaskDetailDTO> GetTask()
        {
            List<TaskDetailDTO> taskList = new List<TaskDetailDTO>();
            var list = (from t in db.TASK
                        join s in db.TASKSTATE on t.TaskState equals s.Id_TaskEstate
                        join e in db.EMPLOYEE on t.ID_Employee equals e.ID_Employee
                        join d in db.DEPARTMENT on e.Department_ID equals d.ID
                        join p in db.POSITION on e.Position_ID equals p.ID
                        select new
                        {
                            taskID = t.ID_Task,
                            title = t.TaskTitle,
                            contenido = t.TaskContent,
                            startDate = t.TaskStartDate,
                            deliveryDate = t.TaskDeliveryDate,
                            taskStateName = s.StateName,
                            taskStateId = s.Id_TaskEstate,
                            userNo = e.UserNo,
                            name = e.Name,
                            employeeId = e.ID_Employee,
                            surName = e.Apellido,
                            positionName = p.PositionName,
                            departmentName = d.DepartmentName,
                            positionId = e.Position_ID,
                            deparmentId = e.Department_ID


                        }).OrderBy(x => x.startDate).ToList();
            foreach (var item in list)
            {
                TaskDetailDTO dto = new TaskDetailDTO();
                dto.TaskID = item.taskID;
                dto.Title = item.title;
                dto.Content = item.contenido;
                dto.TaskStartDate = item.startDate;
                dto.TaskDeliveryDate = item.deliveryDate;
                dto.TaskStateName = item.taskStateName;
                dto.TaskStateID = item.taskStateId;
                dto.UserNo = item.userNo;
                dto.Name = item.name;
                dto.EmployeeId = item.employeeId;
                dto.Surname = item.surName;
                dto.PositionName = item.positionName;
                dto.DepartmentName = item.departmentName;
                dto.PositionID = item.positionId;
                dto.DepartmentID = item.deparmentId;
                taskList.Add(dto);

            }
            return taskList;
        }

        public static void ApproveTask(int taskID, bool isAdmin)
        {
            try
            {
                TASK task = db.TASK.First(x => x.ID_Task == taskID);
                if (isAdmin)
                    task.TaskState = TaskStates.Approve;
                else
                    task.TaskState = TaskStates.Delivered;
                task.TaskDeliveryDate = DateTime.Today;
                db.SubmitChanges();              
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void DeleteTask(int taskID)
        {
            try
            {
                TASK borrarUnoSolo = db.TASK.First(x => x.ID_Task == taskID);
                db.TASK.DeleteOnSubmit(borrarUnoSolo);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static void UpdateTask(TASK task)
        {
            try
            {
                TASK ts = db.TASK.First(x => x.ID_Task == task.ID_Task);
                ts.TaskTitle = task.TaskTitle;
                ts.TaskContent = task.TaskContent;
                ts.TaskState = task.TaskState;
                ts.TaskStartDate = task.TaskStartDate;
                ts.ID_Employee = task.ID_Employee;
                db.SubmitChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
