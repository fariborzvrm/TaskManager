using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyTestApp
{
    internal class TaskRepository
    {
        private string connectionString =
        "Server=(localdb)\\MSSQLLocalDB;Database=TaskManagerDB;Trusted_Connection=True;";

        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        private TaskItem MapTask(SqlDataReader reader)
        {
            return new TaskItem
            {
                Id = (int)reader["Id"],
                Title = reader["Title"].ToString(),
                IsDone = (bool)reader["IsDone"]
            };
        }

        
       

        public async Task AddTaskAsync(string title)
        {

            var task = new TaskItem
            {

                Title = title,
                IsDone = false,

            };

            await _context.AddAsync(task);
            await _context.SaveChangesAsync();
            
                       
        }

        

        public async Task<List<TaskItem>> GetTasksAsync() {            

            return await _context.TaskItems.ToListAsync();
           
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);

            if (task == null) 
                return false; 

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();

            return true;
           
            }
            
        
        public async Task MarkAsDoneAsync(int id)            
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
                return;

            task.IsDone = true;

            await _context.SaveChangesAsync();

           
            }
           
       
        public async Task<List<TaskItem>> GetPendingTasksAsync()
        {
            
           return await _context.TaskItems
                .Where(t => !t.IsDone)
                .ToListAsync();
            
        }

      
    }
}