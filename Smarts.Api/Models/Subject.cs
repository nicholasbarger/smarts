using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    /// <summary>
    /// An educational subject (also used as hashtags for curriculums and educational assets).
    /// </summary>
    public class Subject
    {
        /// <summary>
        /// The primary key in the database and the hashtag itself.
        /// </summary>
        [Key]
        public string Hashtag { get; set; }

        /// <summary>
        /// The contributor of the subject (either explicit or on first use).
        /// </summary>
        public Guid ContributorGuid { get; set; }

        /// <summary>
        /// An optional description for the subject.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// An elongated title for the hashtag.
        /// </summary>
        public string Title { get; set; }
    }
}