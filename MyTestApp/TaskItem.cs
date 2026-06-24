using System;
using System.Collections.Generic;
using System.Text;

namespace MyTestApp
{
    internal class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsDone { get; set; }

        public DateTime CreatedAt { get; set; }= DateTime.Now;
    }
}
