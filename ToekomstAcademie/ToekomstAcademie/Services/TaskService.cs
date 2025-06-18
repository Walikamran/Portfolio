using ToekomstAcademie.Models;

namespace ToekomstAcademie.Services
{
    public class TaskService
    {
        private List<MaintenanceTask> _tasks = new()
        {
            new MaintenanceTask { Id = 1, Titel = "Server update", Beschrijving = "Beveiligingspatch uitvoeren", Status = "ToDo", Deadline = DateTime.Today.AddDays(2) },
            new MaintenanceTask { Id = 2, Titel = "Backup controleren", Beschrijving = "Controleer of de back-ups werken", Status = "InProgress", Deadline = DateTime.Today.AddDays(1) }
        };

        public Task<List<MaintenanceTask>> GetTasksAsync() => Task.FromResult(_tasks);

        public Task AddTaskAsync(MaintenanceTask task)
        {
            task.Id = _tasks.Max(t => t.Id) + 1;
            _tasks.Add(task);
            return Task.CompletedTask;
        }

        public Task UpdateTaskAsync(MaintenanceTask task)
        {
            var existing = _tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existing != null)
            {
                existing.Titel = task.Titel;
                existing.Beschrijving = task.Beschrijving;
                existing.Status = task.Status;
                existing.Deadline = task.Deadline;
            }
            return Task.CompletedTask;
        }

        public Task DeleteTaskAsync(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null) _tasks.Remove(task);
            return Task.CompletedTask;
        }
    }
}
