using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    /// <summary>
    /// An educational subject (also used as hashtags for curriculums and educational assets).
    /// </summary>
    public class Subject
    {
        #region Properties

        /// <summary>
        /// The primary key in the database and the hashtag itself.
        /// </summary>
        [Key]
        public string Hashtag { get; set; }

        /// <summary>
        /// The contributor of the subject (either explicit or on first use).
        /// </summary>
        [ForeignKey("Contributor")]
        public Guid ContributorGuid { get; set; }

        /// <summary>
        /// When the database record was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// An optional description for the subject.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// An elongated title for the hashtag.
        /// </summary>
        public string Title { get; set; }

        #endregion

        #region Virtual Properties

        /// <summary>
        /// The list of assets tagged by this subject hashtag.
        /// </summary>
        public ICollection<Asset> Assets { get; set; }

        /// <summary>
        /// The user who contributed this educational asset.
        /// </summary>
        public virtual WebUser Contributor { get; set; }

        #endregion
    }
}