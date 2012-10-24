using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.BusinessLogic
{
    public class AssetBusinessLogic
    {
        /// <summary>
        /// Set the default values when creating a new educational asset.
        /// </summary>
        /// <param name="obj"></param>
        public void SetDefaults(ref Asset obj)
        {
            // Return on empty
            if (obj == null)
            {
                return;
            }

            // New obj defaults
            if (obj.Id <= 0)
            {
                obj.Created = DateTime.Now;
                obj.IsActive = true;
            }

            // Update child defaults
            if (obj.SubjectAssociations != null)
            {
                for (int i = 0; i < obj.SubjectAssociations.Count; i++)
                {
                    var subjectLogic = new SubjectBusinessLogic();
                    var subject = obj.SubjectAssociations.ElementAt(i);
                    subjectLogic.SetDefaults(ref subject);
                }
            }
        }
    }
}