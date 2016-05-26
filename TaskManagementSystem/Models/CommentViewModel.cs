using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskManagementModule;

namespace TaskManagementSystem.Models
{
    // View model for Comments
    public class CommentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public CommentType CommentType { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? ReminderDate { get; set; }
        public int TaskId { get; set; }
    }
}