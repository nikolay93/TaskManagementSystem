using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementModule;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    // Tasks controller
    public class TasksController : BaseController
    {
        // listing all of the tasks
        // showing opened/closed and due tasks over a specific period
        public ActionResult Index(DateTimeOffset? from, DateTimeOffset? to)
        {
            var tasks = db.Tasks.ToList();
            if (from.HasValue && to.HasValue)
            {
                tasks = db.Tasks.Where(t => (t.CreateDate > from.Value && to.Value > t.CreateDate) || (t.RequiredByDate < to.Value && from.Value < t.RequiredByDate)).ToList();
            }
            return View(tasks);
        }

        // Create task GET action
        public ActionResult Create()
        {
            return View();
        }

        // Create task Post action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskViewModel taskModel)
        {
            if (ModelState.IsValid)
            {
                var task = new Task()
                {
                    TaskName = taskModel.TaskName,
                    TaskDescription = taskModel.TaskDescription,
                    TaskStatus = taskModel.TaskStatus,
                    TaskType = taskModel.TaskType,
                    RequiredByDate = taskModel.RequiredByDate,
                    NextActionDate = taskModel.NextActionDate,
                    CreateDate = DateTime.Today
                };

                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskModel);
        }

        // Edit GET action for fetching the data for specific task from the DB
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            TaskViewModel taskVM = new TaskViewModel()
            {
                TaskName = task.TaskName,
                TaskDescription = task.TaskDescription,
                TaskStatus = task.TaskStatus,
                TaskType = task.TaskType,
                NextActionDate = task.NextActionDate,
                RequiredByDate = task.RequiredByDate
            };
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(taskVM);
        }

        // Post Action for saving the new data for a specific task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskViewModel taskVM)
        {
            if (ModelState.IsValid)
            {
                Task task = db.Tasks.Find(taskVM.Id);
                if (task == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                task.TaskName = taskVM.TaskName;
                task.TaskDescription = taskVM.TaskDescription;
                task.TaskStatus = taskVM.TaskStatus;
                task.TaskType = taskVM.TaskType;
                task.NextActionDate = taskVM.NextActionDate;
                task.RequiredByDate = taskVM.RequiredByDate;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskVM);
        }

        // Details action for desplaying a task and all associated comments
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            var nextAction = new DateTimeOffset?();
            // Take the first comment with date closest to the current date
            var commentsList = task.Comments.Where(d => d.ReminderDate > DateTimeOffset.Now).OrderBy(d => d.ReminderDate).Take(1);

            foreach (var comment in commentsList)
            {
                nextAction = comment.ReminderDate;
            }
            TaskViewModel taskVM = new TaskViewModel()
            {
                Id = task.Id,
                TaskName = task.TaskName,
                TaskDescription = task.TaskDescription,
                TaskStatus = task.TaskStatus,
                TaskType = task.TaskType,
                NextActionDate = nextAction,
                RequiredByDate = task.RequiredByDate
            };

            var taskDetailsViewModel = new TaskDetailsViewModel()
            {
                Task = taskVM,
                Comments = task.Comments.ToList()
            };
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(taskDetailsViewModel);
        }

        // Delete task from DB
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = db.Tasks.Find(id);
            if (task != null)
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }

}

