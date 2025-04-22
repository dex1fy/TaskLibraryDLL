using SimpleTaskLibrary.Models;
using System.Collections.Generic;
using TaskLibraryDLL.Enum;

namespace TaskLibraryDLL.Services
{
    public class TaskManager
    {
        private List<TaskModel> tasks = new List<TaskModel>();

        // Добавляет новую задачу
        public void AddTask(TaskModel task)
        {
            tasks.Add(task);
        }

        // Удаляет задачу по её идентификатору
        public bool RemoveTask(Guid taskId)
        {
            var task = tasks.Find(t => t.Id == taskId);
            if (task != null)
            {
                tasks.Remove(task);
                return true;
            }
            return false;
        }

        // Устанавливает статус задачи
        public bool UpdateTaskStatus(Guid taskId, TaskStatuses newStatus)
        {
            var task = tasks.Find(t => t.Id == taskId);
            if (task != null)
            {
                task.Status = newStatus;
                return true;
            }
            return false;
        }


        // Устанавливает дедлайн задачи
        public bool SetTaskDeadline(Guid taskId, DateTime deadline)
        {
            var task = tasks.Find(t => t.Id == taskId);
            if (task != null)
            {
                task.Deadline = deadline;
                return true;
            }
            return false;
        }

        // Возвращает список всех задач (можно фильтровать)
        public IEnumerable<TaskModel> GetTasks(
            TaskStatuses? statusFilter = null,
            bool? showOverdueOnly = false)
        {
            var query = tasks.AsEnumerable();

            if (statusFilter != null)
                query = query.Where(t => t.Status == statusFilter);

            if (showOverdueOnly == true)
                query = query.Where(t => t.Deadline < DateTime.Now);

            return query.ToList().AsReadOnly();
        }

        // Возвращает минимальную статистику задач
        public (int TotalTasks, int CompletedTasks, int OverdueTasks) GetTaskStatistics()
        {
            int total = tasks.Count;
            int completed = tasks.Count(t => t.Status == TaskStatuses.Completed);
            int overdue = tasks.Count(t => t.Deadline < DateTime.Now);

            return (total, completed, overdue);
        }
    }
}