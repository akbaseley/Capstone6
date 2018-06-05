using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anna_Baseley_Capstone_6.Models;

namespace Anna_Baseley_Capstone_6.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult RegisterNewUser()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult AddUser(User newUser)

        {
            if (ModelState.IsValid)
            {
                TaskManagerDBEntities2 ORM = new TaskManagerDBEntities2();
                ORM.Users.Add(newUser);
                ORM.SaveChanges();
                return View("TaskList");
            }
            else
            {
                ViewBag.Message = "Oops!  It looks as though something has gone wrong!";
                return View("Error");
            }
        }

        public ActionResult UserLogin(string UserID, string UserPassword)
        {
            TaskManagerDBEntities2 ORM = new TaskManagerDBEntities2();

            User currentUser = ORM.Users.Find(UserID);
            
            if (currentUser == null)
            {
                ViewBag.ErrorMessage = "UserID was not found";
                return View("Index");
            }

            else if (currentUser.UserPassword != UserPassword)
            {
                ViewBag.ErrorMessage = "Incorrect Password!";
                return View("Index");
            }

            TempData.Add("AuthorizedUser", currentUser);

            TempData["AuthorizedUser"] = currentUser;
         
            return RedirectToAction("TaskList");
            
        }

        public ActionResult TaskList()
        {
            TaskManagerDBEntities2 ORM = new TaskManagerDBEntities2();

            User currentUser = (User)TempData["AuthorizedUser"];
            ViewBag.UserTasks = ORM.Users.Find(currentUser.UserID).Tasks;

            ViewBag.AuthorizedUser = currentUser;
            TempData["AuthorizedUser"] = currentUser;

            return View();
        }

        public ActionResult TaskForm()
        {

            if (TempData["AuthorizedUser"] != null)
            {
                ViewBag.AuthorizedUser = TempData["AuthorizedUser"];

                TempData["AuthorizedUser"] = TempData["AuthorizedUser"];

                return View();
            }
            else
            {
                ViewBag.ErrorMessage = "Please sign in!";
                return View("Index");
            }

        }

        [ValidateAntiForgeryToken]
        public ActionResult AddNewTask(Task newTask)
        {
            ViewBag.AuthorizedUser = TempData["AuthorizedUser"];

            TaskManagerDBEntities2 ORM = new TaskManagerDBEntities2();
            ORM.Tasks.Add(newTask);
            ORM.SaveChanges();

            TempData["AuthorizedUser"] = TempData["AuthorizedUser"];

            return RedirectToAction("TaskList");
        }

        public ActionResult DeleteTask(string TaskName)
        {
            TaskManagerDBEntities2 ORM = new TaskManagerDBEntities2();

            Task userTask = ORM.Tasks.Find(TaskName);

            if(userTask != null)
            {
                ORM.Tasks.Remove(userTask);
                ORM.SaveChanges();

                TempData["AuthorizedUser"] = TempData["AuthorizedUser"];

                return RedirectToAction("TaskList");
            }
            else
            {
                ViewBag.ErrorMessage = "Task not found!";
                return View("Error");
            }
        }

        public ActionResult MarkComplete(Task updatedTask)
        {
            TaskManagerDBEntities2 ORM = new TaskManagerDBEntities2();

            Task oldTaskRecord = ORM.Tasks.Find(updatedTask.TaskName);

            if(oldTaskRecord.Complete == "Not Complete")
            {
                updatedTask.Complete = "Complete";

            }
            else
            {
                updatedTask.Complete = "Not Complete";
            }

            oldTaskRecord.Complete = updatedTask.Complete;
            ORM.Entry(oldTaskRecord).State = System.Data.Entity.EntityState.Modified;
            ORM.SaveChanges();

            TempData["AuthorizedUser"] = TempData["AuthorizedUser"];

            return RedirectToAction("TaskList");
        }

        public ActionResult LogOut()
        {
            
            TempData["AuthorizedUser"] = null;

            return RedirectToAction("Index");
        }
    }
}