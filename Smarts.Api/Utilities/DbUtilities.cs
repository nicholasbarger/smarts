using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Smarts.Api.Utilities
{
    public class DbUtilities
    {
        public dynamic Map(object original, object target)
        {
            // if original is null and target is not, return target
            if (original == null)
            {
                return target;
            }

            // if original has value and target is null, return original <-- this means you can't empty out a value
            if (original != null && target == null)
            {
                return original;
            }

            // if original and target do not match, return target
            if (!original.Equals(target))
            {
                return target;
            }

            // if original and target match, return original
            if (original.Equals(target))
            {
                return original;
            }

            return null;            
        }

        public void SaveWithExpectedSuccess(int result)
        {
            if (result <= 0)
            {
                throw new DbUpdateException("Database attempted save, but returned <=0 (failure).");
            }
        }
    }
}