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

        // Возвращает список всех задач
        public IEnumerable<TaskModel> GetAllTasks()
        {
            return tasks.AsReadOnly();
        }
    }
}