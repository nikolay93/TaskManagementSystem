using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementModule;

namespace TaskManagementSystem.Controllers
{
    // Base controller containing the DB connection instance and dispose method
    public abstract class BaseController : Controller
    {
        protected readonly TaskManagementEntitiesContainer db = new TaskManagementEntitiesContainer();
      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}