using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    /// <summary>
    /// A structure of assets provided and organized by the community.
    /// </summary>
    public class Curriculum
    {
        #region Properties

        /// <summary>
        /// The unique id for the database record.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The contributor of the curriculum (other users may contribute assets to the curriculum).
        /// </summary>
        public Guid ContributorGuid { get; set; }

        /// <summary>
        /// The description of the curriculum (overall purpose/direction).
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The title of the curriculum.
        /// </summary>
        public string Title { get; set; }

        #endregion
    }
}