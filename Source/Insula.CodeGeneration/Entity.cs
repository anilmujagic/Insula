using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Insula.CodeGeneration
{
    public class Entity
    {
        public string Name { get; set; }
        public string Schema { get; set; }
        public string Database { get; set; }
        public EntityMemberCollection Members { get; private set; }
        public Collection<string> Attributes { get; private set; }

        public Entity()
        {
            this.Members = new EntityMemberCollection();
            this.Attributes = new Collection<string>();
        }
    }
}
