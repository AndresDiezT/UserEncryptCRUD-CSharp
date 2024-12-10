using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserEncrypt.Context;
using UserEncrypt.Models;

namespace UserEncrypt.Controllers
{
    public class UsersController : Controller
    {
        private UserEncryptContext db = new UserEncryptContext();

        // GET: Users
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,PasswordHash,HashKey,HashIV,CreatedAt")] User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.Find(user.Id);
                if (existingUser == null)
                {
                    return HttpNotFound();
                }

                existingUser.Username = user.Username;
                existingUser.PasswordHash = user.PasswordHash;
                existingUser.HashKey = user.HashKey;
                existingUser.HashIV = user.HashIV;
                existingUser.CreatedAt = user.CreatedAt;

                db.Entry(existingUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Users");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
