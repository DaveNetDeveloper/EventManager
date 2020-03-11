using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace EventManager.Controllers
{
    [HandleError]
    public class EventController : Controller, IDbController
    {
        public EventController() { } 
        public ActionResult Index()
        { 
            var allEvents = GetEvents();
            ViewData.Model = allEvents;
             
            Session["allDataList"] = allEvents;

            ViewData["allEvents"] = allEvents;
            ViewData["firstEvent"] = allEvents.FirstOrDefault();
            
            return View(((IEnumerable<Event>)Session["allDataList"]));
        } 
        public ActionResult Create()
        {
            Session["selectCompanies"] = Session["selectCompanies"] ?? GetSelectedCompanies();
            Session["selectLanguages"] = Session["selectLanguages"] ?? GetSelectedLanguages();

            var e = new Event {
                Created = DateTime.Now,
                Sessions = 0,
                Capacity = 0,
                EventDateTime = DateTime.Now,
                Active = true
            }; 
            return View(e);
        }
        [HandleError]
        public ActionResult Edit(int id)
        {
            //var _event = new Event() { Id = 2, Name = "Evento modificado", Updated = DateTime.Now };
            //Update(_event); 
            //
            //Fill Combos
            Session["selectCompanies"] = Session["selectCompanies"] ?? GetSelectedCompanies();
            Session["selectLanguages"] = Session["selectLanguages"] ?? GetSelectedLanguages();
            
            Session["Id"] = (Session["Id"] == null || (int)Session["Id"] != id) ? id : (int)Session["Id"];
            
            Session["event"] = Get((int)Session["Id"]);

            return View((Event)Session["event"]);
        }
        public ActionResult Disable(List<int> eventIds)
        {
            //DisableEvents(eventIds);
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Event", action = "Index" }));
        } 
        public ActionResult Delete(int id)
        {
            Remove(id);
            //var allEvents = GetEvents();

            //ViewData["allDataList"] = allEvents;
            //ViewData.Model = allEvents; 

            //Session["reCharge"] = true; 
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Event", action = "Index"}));
        }
        public IEnumerable<IDbEntity> GetEvents()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Event.ToList();
                //.Include("Company")
                //.Include("Language").ToList();
            }
        } 
        public IDbEntity Get(int id)
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
        public int AddNew(Event entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var newEvent = new Event
                {
                    Name = entity.Name ?? "Event name",
                    Description = entity?.Description ?? "...",
                    Capacity = entity.Capacity != null ? entity.Capacity : 0,
                    Sessions = entity.Sessions != null ? entity.Sessions : 0,
                    EventDateTime = entity.EventDateTime ?? DateTime.Now,
                    Created = DateTime.Now,
                    Active = true,
                    CompanyId = entity.CompanyId,
                    LanguageId = entity.LanguageId
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
        public bool Update(Event entity)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var entityToUpdate = context.Event.SingleOrDefault(p => p.Id == entity.Id);

                entityToUpdate.Name = entity.Name;
                entityToUpdate.Description = entity.Description;
                entityToUpdate.Capacity = entity.Capacity;
                entityToUpdate.Sessions = entity.Sessions;
                entityToUpdate.EventDateTime = entity.EventDateTime;
                entityToUpdate.Active = entity.Active;
                entityToUpdate.CompanyId = entity.CompanyId;
                entityToUpdate.LanguageId = entity.LanguageId;
                entityToUpdate.Updated = DateTime.Now;
                context.SaveChanges();
            }
            return true;
        }
         
        //privates
        protected List<SelectListItem> GetSelectedCompanies()
        {
            var selectedList = new List<SelectListItem>();
            ((List<Company>) GetCompanies()).ForEach(c =>

                selectedList.Add(new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })); 
            return selectedList;
        } 
        protected List<SelectListItem> GetSelectedLanguages()
        {
            var selectedList = new List<SelectListItem>();
            ((List<Language>)GetLanguages()).ForEach(l =>

               selectedList.Add(new SelectListItem
               {
                   Value = l.Id.ToString(),
                   Text = l.Name
               }));
            return selectedList;
        }
        private IEnumerable<IDbEntity> GetCompanies()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Company.ToList(); 
            }
        }
        private IEnumerable<IDbEntity> GetLanguages()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Language.ToList(); 
            }
        } 
    }
}