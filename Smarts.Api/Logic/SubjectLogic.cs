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
    internal class SubjectLogic
    {
        /// <summary>
        /// Set the default values when creating a new subject tag.
        /// </summary>
        /// <param name="subject"></param>
        public void SetDefaults(ref Subject subject)
        {
            subject.Created = DateTime.Now;
        }
    }
}