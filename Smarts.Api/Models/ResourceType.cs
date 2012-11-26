using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    /// <summary>
    /// A category for types of educational resources such as Articles, Videos, Courseware, etc.
    /// </summary>
    public class ResourceType
    {
        #region Properties

        /// <summary>
        /// The unique id for the database record.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The description of the resource type.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The name of the resource type.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}