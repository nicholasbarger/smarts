using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    public class Comment
    {
        #region Properties

        /// <summary>
        /// The unique id for the database record.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The educational resource the comment is associated with (optional).
        /// </summary>
        public int? ResourceId { get; set; }

        /// <summary>
        /// The contributor that specified the comment.
        /// </summary>
        [ForeignKey("Contributor")]
        public Guid ContributorGuid { get; set; }

        /// <summary>
        /// When the database record was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The actual comment text stated by the user.
        /// </summary>
        public string Text { get; set; }

        #endregion

        #region Virtual Properties

        public virtual WebUser Contributor { get; set; }

        #endregion
    }
}