using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Insula.CodeGeneration
{
    public class EntityCollection : List<Entity>
    {
        public EntityCollection()
        {
        }

        public Entity this[string entityName]
        {
            get
            {
                return this.Single(c => c.Name == entityName);
            }
        }
    }
}
