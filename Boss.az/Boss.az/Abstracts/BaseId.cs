using System;

namespace Boss.az.UniqueIdNS
{
    abstract class BaseId
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}

