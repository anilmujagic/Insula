using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Insula.CodeGeneration
{
    public class EntityMemberCollection : List<EntityMember>
    {
        public EntityMemberCollection()
        {
        }

        public EntityMember this[string entityMemberName]
        {
            get
            {
                return this.Single(c => c.Name == entityMemberName);
            }
        }
    }
}
