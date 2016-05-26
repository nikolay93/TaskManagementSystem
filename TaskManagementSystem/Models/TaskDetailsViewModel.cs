using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManagementModule;

namespace TaskManagementSystem.Models
{
    // View model for the task and associated comments use in the Details page
    public class TaskDetailsViewModel
    {
        public TaskViewModel Task { get; set; }
        public List<Comment> Comments { get; set; }
    }
}