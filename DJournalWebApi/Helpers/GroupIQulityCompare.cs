using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DJournalWebApi.Model;

namespace DJournalWebApi.Helpers
{
    public class GroupIQulityCompare : EqualityComparer<DJournalWebApi.Model.Group>
    {
        public override bool Equals(Group x, Group y)
        {
            return x.NewName == y.NewName || x.OldName == y.OldName;
        }

        public override int GetHashCode(Group obj)
        {
            return obj.GetHashCode();
        }
    }
}
