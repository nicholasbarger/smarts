using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.BusinessLogic
{
    public class TopicBusinessLogic
    {
        /// <summary>
        /// Set the default values when creating a new subject tag.
        /// </summary>
        /// <param name="subject"></param>
        public void SetDefaults(ref ResourceToTopicAssociation subject)
        {
            subject.Created = DateTime.Now;
        }
    }
}