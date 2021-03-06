﻿using EventManager.Common;
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
        
        [Authorize]
        public ActionResult Index()
        { 
            var allEvents = GetEvents();
            ViewData.Model = allEvents;
             
            Session["allDataList"] = allEvents;

            ViewData["allEvents"] = allEvents;
            ViewData["firstEvent"] = allEvents.FirstOrDefault();
            
            return View((IEnumerable<Event>)Session["allDataList"]);
        }
        [Authorize]
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
        [Authorize] [HandleError]
        public ActionResult Edit(int id)
        {
            //Fill Combos
            Session["selectCompanies"] = Session["selectCompanies"] ?? GetSelectedCompanies();
            Session["selectLanguages"] = Session["selectLanguages"] ?? GetSelectedLanguages();
            
            Session["Id"] = (Session["Id"] == null || (int)Session["Id"] != id) ? id : (int)Session["Id"]; 
            Session["event"] = Get((int)Session["Id"]);

            return View((Event)Session["event"]);
        }
        [Authorize]
        public ActionResult Disable(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Event.Where(e => id == e.Id).ToList().ForEach(e => e.Active = false); 
                context.SaveChanges();
            }  
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Event", action = "Index" }));
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            Remove(id);
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
                    Active = entity.Active,
                    CompanyId = entity.CompanyId,
                    LanguageId = entity.LanguageId,
                    Created = DateTime.Now
                };

                context.Event.Add(newEvent);
                context.SaveChanges();
                return newEvent.Id;
            }
        } 
        public void Remove(int id)
        {
            try
            { 
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    var entityToRemove = context.Event.SingleOrDefault(p => p.Id == id);
                    context.Event.Remove(entityToRemove);
                    context.SaveChanges(); 
                }
                Session["ConfirmationDelete"] = Constants.ActionResult_Success;
            }
            catch
            {
                Session["ConfirmationDelete"] = Constants.ActionResult_Error;
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
        public void ResetSession(string name)
        {
            Session[name] = null;
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