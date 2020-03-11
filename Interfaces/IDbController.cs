using System.Collections.Generic;
using System.Web.Mvc;

namespace EventManager.Controllers
{
    internal interface IDbController
    {
        int AddNew(Event pEvent);
        ActionResult Edit(int id);
        IDbEntity Get(int id);
        IEnumerable<IDbEntity> GetEvents();
        bool Update(Event entity);
        ActionResult Delete(int id);
    }
}