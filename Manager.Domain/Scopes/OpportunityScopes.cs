using Manager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Domain.Scopes
{
    public static class OpportunityScopes
    {
        public static bool NewScopesValid(this Opportunity opportunity)
        {
            return false;
        }
    }
}
