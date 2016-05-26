using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementModule;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    // Comments controller
    public class CommentsController : BaseController
    {
        // listing all of the comments
        // searching comments according to specific criteria
        public ActionResult Search(string searchQuery, string searchCriteria)
        {
            var comments = db.Comments.ToList();
            switch (searchCriteria)
            {
                case "Date added":
                    comments = comments.Where(c => c.DateAdded == DateTimeOffset.Parse(searchQuery)).ToList();
                    break;
                case "Type":
                    CommentType ct = (CommentType)Enum.Parse(typeof(CommentType),searchQuery,true);
                    comments = comments.Where(c => c.CommentType == ct ).ToList();
                    break;
                case "Text":
                    comments = comments.Where(c => c.Text.Contains(searchQuery)).ToList();
                    break;
                case "Reminder date":
                    comments = comments.Where(c=> c.ReminderDate == DateTimeOffset.Parse(searchQuery)).ToList();
                    break;
            }
            return View(comments);
        }

        // Create comment Get action
        public ActionResult Create(int id)
        {
            var commentModel = new CommentViewModel();
             commentModel.TaskId = id;
            return View(commentModel);
        }

        // Post action for creating a comment in the DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentViewModel commentModel)
        {
            Task task = db.Tasks.Find(commentModel.TaskId);
            if (task == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var comment = new Comment()
                {
                    DateAdded = DateTime.Today,
                    Text = commentModel.Text,
                    CommentType = commentModel.CommentType,
                    ReminderDate = commentModel.ReminderDate,
                    TaskId = commentModel.TaskId
                };
                task.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details","Tasks", new { id = commentModel.TaskId });
            }
            return View(commentModel);
        }
        
        // Edit GET action for fetching the data for specific comment from the DB
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            CommentViewModel commentVM = new CommentViewModel()
            {
                Text = comment.Text,
                CommentType = comment.CommentType,
                ReminderDate = comment.ReminderDate,
                TaskId = comment.TaskId
            };
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(commentVM);
        }
        
        // Post action for saving the new data for a specific comment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CommentViewModel commentVM, string returnPage = "Tasks/Details")
        {
            if (ModelState.IsValid)
            {
                var comment = db.Comments.Find(commentVM.Id);
                var taskid = comment.TaskId;
                if (comment == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
               comment.Text = commentVM.Text;
               comment.ReminderDate = commentVM.ReminderDate;
               comment.CommentType = commentVM.CommentType;
                db.SaveChanges();
                if (returnPage == "Tasks/Details")
                {
                    return RedirectToAction("Details", "Tasks", new { id = taskid });
                }
                else
                {
                    return RedirectToAction("Search", "Comments");
                }
            }
            return View(commentVM);
        }

        // Delete comment from DB
        public ActionResult Delete(int id, string returnPage = "Tasks/Details")
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comment = db.Comments.Find(id);
            var taskid = comment.TaskId;
            if (comment != null)
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
                if (returnPage == "Tasks/Details")
                {
                    return RedirectToAction("Details", "Tasks", new { id = taskid });
                }
                else {
                    return RedirectToAction("Search", "Comments");
                }
            }
            return View();
        }
    }
}