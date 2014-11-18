using System;
using System.Collections.Generic;
using System.Text;

namespace QuickBooksAgent.Domain
{
    public partial class EntityState
    {
        public static EntityState Synchronized = new EntityState(1, "Synchronized");
        public static EntityState Modified = new EntityState(2,"Modified");
        public static EntityState Deleted = new EntityState(3,"Deleted");

        public static EntityState Created = new EntityState(0,"Created");

        public EntityState() { }
        
        public static bool operator == (EntityState arg1, EntityState arg2)
        {
            return arg1.Equals(arg2);
        }
        
        public static bool operator != (EntityState arg1, EntityState arg2)
        {
            return !arg1.Equals(arg2);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is EntityState)
                return (obj as EntityState).EntityStateId == EntityStateId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
