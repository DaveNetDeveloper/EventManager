using System.Collections.Generic;
using System.Web.Mvc;

namespace EventManager.Controllers
{
    internal interface IDbController
    {
        int CreateEvent(Event pEvent);
        ActionResult Edit(int id);
        IDbEntity Get(int id);
        IEnumerable<IDbEntity> GetEvents();
        bool Update(IDbEntity entity);
        ActionResult Delete(int id);
    }
}