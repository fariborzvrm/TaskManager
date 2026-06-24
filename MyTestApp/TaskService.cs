using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MyTestApp
{
    internal class TaskService
    {
        private TaskRepository _repository;

        public TaskService(TaskRepository repository)
        {
            _repository = repository;
        }

        private async Task LogActionAsync(string message)
        {
            string path = "tasks_log.txt";
            if (!File.Exists(path))
            {
                Console.WriteLine("Creating Log File...");
                
            }
            await File.AppendAllTextAsync(path, message);
        }

        public async Task<List<TaskItem>> SearchTasksAsync (string keyword)
        {
            var list = await _repository.GetTasksAsync();
            
            return list
                .Where(t => t.Title.Contains(keyword))
                .ToList();
        }

        public async Task AddTaskAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty");
                return;
            }


           if (title.Length < 3)
            {
                Console.WriteLine("Title must be at least 3 characters");
                return;
            }
            await _repository.AddTaskAsync(title);
            await LogActionAsync($"{DateTime.Now:yyyy-MM-dd HH:mm} Task added: {title}\n");
            
        }

        public async Task<List<TaskItem>> GetTasksAsync()
        {
            return await _repository.GetTasksAsync();
        }

        public async Task<List<TaskItem>> GetPendingTasksAsync()
        {
              return await _repository.GetPendingTasksAsync();
        }

        public async Task<List<TaskItem>> GetCompletedTasksAsync()
        {
            var tasks = await _repository.GetTasksAsync();

            return tasks
                .Where(t=> t.IsDone)
                .ToList();
        }
        public async Task<List<TaskItem>> GetTasksSortedByTitleAsync()
        {
            var tasks = await _repository.GetTasksAsync();

            return tasks
                .OrderBy(t=> t.Title)
                .ToList();
        }

        public async Task<List<string>> GetTaskTitlesAsync()
        {
            var tasks = await _repository.GetTasksAsync();
            return tasks
                .Select(t => t.Title)
                .ToList();
        }

      
        public async Task<List<string>> GetPendingTaskTitlesAsync()
        {
            var tasks =await _repository.GetTasksAsync();

            return tasks
                .Where (t=> !t.IsDone)
                .OrderBy (t=> t.Title)
                .Select (t=> t.Title)
                .ToList ();
        }


        public async Task<bool> DeleteTaskAsync(int id)
        {
            if (id <= 0)
            {                
             return false ;
            }
            await _repository.DeleteTaskAsync(id);
            return true ;
        }

        public async Task<bool> MarkAsDoneAsync(int id)
        {
            if (id <= 0)
            {              
                return false;
            }
            await _repository.MarkAsDoneAsync(id);
            return true;
            
        }
    }
}
