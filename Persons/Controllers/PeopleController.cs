using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Persons.DAL;
using Persons.Models;
using System.Text.RegularExpressions;

namespace Persons.Controllers
{
    public class PeopleController : Controller
    {
        private PeopleContex db = new PeopleContex();

        // GET: People
        public ActionResult Index(string search, string order) 
        {
            var people = from p in db.People
                         select p;

            ViewBag.IsSearch = false;
            if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
            {
                ViewBag.IsSearch = true;
                people = Search(people, search);
                ViewBag.Search = search;
            }

            if (!string.IsNullOrEmpty(order))
            {
                people = Order(people, order);

                ViewBag.Sort = order;
            }

            ViewBag.NumberSort = string.IsNullOrEmpty(order) ? "number_desc" : string.Empty;
            ViewBag.FirstNameSort = order == "fName_asc" ? "fName_desc" : "fName_asc";
            ViewBag.LastNameSort = order == "lName_asc" ? "lName_desc" : "lName_asc";

            if (people.Count() > 0)
            {
                ViewBag.Numbers = GetNumbers(people.ToList()); 
            }

            ViewBag.Names = GetNames(db.People.ToList());
            ViewBag.Ids = GetIds(db.People.ToList());

            return View(people.ToList());
        }

        private IQueryable<Person> Order(IQueryable<Person> people, string order)
        {
            switch (order)
            {
                case "number_desc":
                    people = people.OrderByDescending(p => p.PersonId);
                    break;
                case "fName_asc":
                    people = people.OrderBy(p => p.FirstName);
                    break;
                case "fName_desc":
                    people = people.OrderByDescending(p => p.FirstName);
                    break;
                case "lName_asc":
                    people = people.OrderBy(p => p.LastName);
                    break;
                case "lName_desc":
                    people = people.OrderByDescending(p => p.LastName);
                    break;
            }

            return people;
        }

        private IQueryable<Person> Search(IQueryable<Person> people, string search)
        {
            search = Regex.Replace(search, " ", string.Empty);
            people = people.Where(p => (p.FirstName + p.LastName).Contains(search));
            return people;
        }

        private string[] GetNames(List<Person> people)
        {
            string[] names = new string[people.Count()];
            for (int index = 0; index < names.Length; index++)
            {
                names[index] = people[index].FirstName + " " + people[index].LastName;
            }

            return names;
        }

        private int[] GetIds(List<Person> people)
        {
            int[] ids = new int[people.Count()];
            for (int index = 0; index < ids.Length; index++)
            {
                ids[index] = people[index].PersonId;
            }

            return ids;
        }

        // GET: People/Details/5
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonId,FirstName,LastName")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: People/Edit/5
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonId,FirstName,LastName")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
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
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private int[] GetNumbers(List<Person> people)
        {
            int[] numbers = new int[people.Count()];

            numbers[people.IndexOf(people.Min())] = 1;
            int startNumber = people.Min().PersonId;

            for (int index = 1, number = 2; index < numbers.Length; index++, number++)
            {
                int i = FindIndex(people, ref startNumber);
                numbers[i] = number;
            }

            return numbers;
        }

        private int FindIndex(List<Person> people, ref int startNumber)
        {
            int searchId = startNumber + 1, max = people.Max().PersonId;
            while (searchId <= max)
            {
                foreach (var person in people)
                {
                    if (person.PersonId == searchId)
                    {
                        startNumber = searchId;
                        return people.IndexOf(person);
                    }
                }
                searchId++;
            }
            return -1;
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
