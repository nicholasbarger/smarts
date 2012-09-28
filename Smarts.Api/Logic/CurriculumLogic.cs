using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.Logic
{
    /// <summary>
    /// Place all business logic methods in here.
    /// </summary>
    internal class CurriculumLogic
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