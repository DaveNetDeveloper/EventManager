using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EventManager.Controllers
{
    [HandleError]
    public class EventController : Controller, IEntityController
    {
        public EventController() { }

        public ActionResult Index()
        {
            if (Session["allDataList"] == null)
            {
                var allEvents = GetEvents();
                ViewData.Model = allEvents;

                //TempData.Add("allDataList", allEvents);
                Session["allDataList"] = allEvents;

                ViewData["allEvents"] = allEvents;
                ViewData["firstEvent"] = allEvents.FirstOrDefault();
            }
            return View(((IEnumerable<Event>)Session["allDataList"]));
        }
        public ActionResult AddNew(Event e)
        {
            Create(e);

            var allEvents = GetEvents();
            ViewData["allEvents"] = allEvents;
            ViewData.Model = allEvents;
            Session["allDataList"] = allEvents;

            return View("Index");
        }

        [HandleError]
        public ActionResult Edit(int id)
        {
            //var _event = new Event() { Id = 2, Name = "Evento modificado", Updated = DateTime.Now };
            //Update(_event); 
             
            Session["selectCompanies"] = Session["selectCompanies"] ?? GetSelectedCompanies();
            Session["Id"] = (Session["Id"] == null || (int)Session["Id"] != id) ? id : (int)Session["Id"];
            Session["event"] = Get((int)Session["Id"]);

            return View((Event)Session["event"]);
        }
        public bool Delete(int id)
        {
            Remove(id);
            var allEvents = GetEvents();

            ViewData["allEvents"] = allEvents;
            ViewData.Model = allEvents;
            Session["allDataList"] = allEvents;

            return true;
        }
        public IEnumerable<IEntity> GetEvents()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Event.ToList();
                //.Include("Company")
                //.Include("Language").ToList();
            }
        }
        public IEnumerable<IEntity> GetCompanies()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Company.ToList();
                //.Include("Company")
                //.Include("Language").ToList();
            }
        }
        public IEntity Get(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return (context.Event
                        //.Include("Company")
                        //.Include("Language")
                        .SingleOrDefault(e => e.Id == id));
            }
        }
        public Event Get(string eventName)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return (from p in context.Event
                        where p.Name.Equals(eventName)
                        select p)
                    .FirstOrDefault();
            }
        }
        public int Create(IEntity entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var newEvent = new Event
                {
                    Name = ((Event)entity).Name ?? "Event name",
                    Description = ((Event)entity)?.Description ?? "...",
                    Capacity = ((Event)entity).Capacity != null ? ((Event)entity).Capacity : 10,
                    EventDateTime = ((Event)entity).EventDateTime ?? DateTime.Now.AddHours(18),
                    Created = DateTime.Now,
                    CompanyId = 1,
                    LanguageId = 1
                };
                context.Event.Add(newEvent);
                context.SaveChanges();
                return newEvent.Id;
            }
        }
        public void Remove(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var entityToRemove = context.Event.SingleOrDefault(p => p.Id == id);
                context.Event.Remove(entityToRemove);
                context.SaveChanges();
            }
        }
        public bool Update(IEntity entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var entityToUpdate = context.Event.SingleOrDefault(p => p.Id == ((Event)entity).Id);

                entityToUpdate.Name = ((Event)entity)?.Name;
                entityToUpdate.Updated = DateTime.Now;
                context.SaveChanges();
            }
            return true;
        }
         
        private List<SelectListItem> GetSelectedCompanies()
        {
            var selectedList = new List<SelectListItem>();
            ((List<Company>) GetCompanies()).ForEach(c =>

                selectedList.Add(new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = false
                })); 
            return selectedList;
        } 
    }
}