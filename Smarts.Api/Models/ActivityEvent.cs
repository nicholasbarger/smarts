using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    /// <summary>
    /// A specific (important) event that is being tracked for auditing, business intelligence, or troubleshooting.
    /// </summary>
    [Table("Events")]
    public class ActivityEvent
    {
        #region Properties

        /// <summary>
        /// The unique id for the database record.
        /// </summary>
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// When the database record was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The description of the event.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The textual name of the event.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }

    public enum ActivityEventItem
    {
        /// <summary>
        /// Found in the app logic when successfully logging in.
        /// </summary>
        Login = 1,

        Logout = 2,
        
        /// <summary>
        /// Found in the app logic when successfully creating a new user.
        /// </summary>
        Enroll = 3,

        /// <summary>
        /// Currently not implemented, thinking I should keep this to google analytics.
        /// </summary>
        PageHit = 4,
        AssetViewed = 5,
        AssetCompleted = 6,
        TagCreated = 7,
        AssetCommented = 8,
        AssetMappedToCurriculum = 9,
        AssetMappedToSubject = 10,
        CurriculumMappedToSubject = 11,

        /// <summary>
        /// Found in the app logic when an existing user is updated (saved).
        /// </summary>
        ProfileUpdated = 12,
        PasswordChanged = 13,
        NotificationsChanged = 14,
        ContactedUs = 15,
        InterviewRequested = 16,
        JobAwarded = 17,
        ProfileViewedByOthers = 18,
        ViewedOtherUserProfile = 19,

        /// <summary>
        /// Found in the app logic when a failed login attempt occurs.
        /// </summary>
        LoginFailed = 20
    }
}