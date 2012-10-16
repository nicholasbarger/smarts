﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    /// <summary>
    /// An entry into the audit table describing something that happened on the website.  Used for business intelligence, troubleshooting, and general auditing.
    /// </summary>
    [Table("Activities")]
    public class Activity
    {
        #region Properties

        /// <summary>
        /// The primary database key to uniquely identify this record.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// An optional reference to an asset.
        /// </summary>
        [ForeignKey("Asset")]
        public int? AssetId { get; set; }

        /// <summary>
        /// The date/time the activity was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// A reference to the type of event this activity is.
        /// </summary>
        [ForeignKey("ActivityEvent")]
        public int EventId { get; set; }

        /// <summary>
        /// An optional reference to a user performing the activity.
        /// </summary>
        [ForeignKey("WebUser")]
        public Guid? UserGuid { get; set; }

        /// <summary>
        /// The textual description of what the activity was and any information about the state of the activity.
        /// </summary>
        public string Value { get; set; }

        #endregion

        #region Virtual Properties

        /// <summary>
        /// The activity event.
        /// </summary>
        public virtual ActivityEvent Event { get; set; }

        /// <summary>
        /// The asset.
        /// </summary>
        public virtual Asset Asset { get; set; }

        /// <summary>
        /// The web user.
        /// </summary>
        public virtual WebUser User { get; set; }

        #endregion
    }
}