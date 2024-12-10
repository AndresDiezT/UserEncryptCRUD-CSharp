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
    public class PeopleController : Controller
    {
        private UserEncryptContext db = new UserEncryptContext();

        // GET: People
        [Authorize]
        public ActionResult Index()
        {
            return View(db.People.ToList());
        }

        // GET: People/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,IdentificationNumber,Email,DocumentType")] Person person)
        {
            if (ModelState.IsValid)
            {

                if (db.People.Any(p => p.IdentificationNumber == person.IdentificationNumber))
                {
                    ModelState.AddModelError("IdentificationNumber", "El número de identificación ya existe.");
                    return View(person);
                }
                if (db.People.Any(p => p.Email == person.Email))
                {
                    ModelState.AddModelError("Email", "Ya hay una persona registrada con este correo.");
                    return View(person);
                }

                var newPerson = new Person
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    DocumentType = person.DocumentType,
                    IdentificationNumber = person.IdentificationNumber,
                    CreatedAt = DateTime.UtcNow
                };

                db.People.Add(newPerson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: People/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,IdentificationNumber,Email,CreatedAt,DocumentType")] Person person)
        {
            if (ModelState.IsValid)
            {
                var personExist = db.People.Find(person.Id);

                if (personExist == null)
                {
                    return HttpNotFound();
                }

                personExist.FirstName = person.FirstName;
                personExist.LastName = person.LastName;
                personExist.Email = person.Email;
                personExist.IdentificationNumber = person.IdentificationNumber;
                personExist.CreatedAt = person.CreatedAt;
                personExist.DocumentType = person.DocumentType;

                db.Entry(personExist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
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