using System;

namespace EventManager
{
    internal interface IAuditEntity
    {
        DateTime Created { get; set; }
        DateTime? Updated { get; set; }
    }
}