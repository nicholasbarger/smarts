using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.BusinessLogic
{
    public class CurriculumBusinessLogic
    {
        /// <summary>
        /// Set the default values when creating a new subject tag.
        /// </summary>
        /// <param name="subject"></param>
        public void SetDefaults(ref Curriculum curriculum)
        {
            curriculum.Created = DateTime.Now;
        }
    }
}