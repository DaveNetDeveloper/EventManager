﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace EventManager.Controllers
{
    internal interface IDbController
    {
        int Create(IDbEntity pEvent);
        ActionResult Edit(int id);
        IDbEntity Get(int id);
        IEnumerable<IDbEntity> GetEvents();
        bool Update(IDbEntity entity);
        bool Delete(int id);
    }
}