using System.Collections.Generic;
using System.Web.Mvc;

namespace EventManager.Controllers
{
    internal interface IEntityController
    {
        int Create(IEntity pEvent);
        ActionResult Edit(int id);
        IEntity Get(int id);
        IEnumerable<IEntity> GetEvents();
        bool Update(IEntity entity);
        bool Delete(int id);
    }
}