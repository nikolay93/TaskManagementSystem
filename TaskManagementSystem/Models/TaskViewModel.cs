using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskManagementModule;

namespace TaskManagementSystem.Models
{
    // View model for tasks
    public class TaskViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? RequiredByDate { get; set; }
        [Required]
        public string TaskName { get; set; }
        [Required]
        public string TaskDescription { get; set; }
        [Required]
        public TaskStatus TaskStatus { get; set; }
        [Required]
        public TaskType TaskType { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? NextActionDate { get; set; }
    }
}