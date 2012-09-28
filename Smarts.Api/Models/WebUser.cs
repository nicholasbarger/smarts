using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    /// <summary>
    /// The users of the system.  These are referred to as Users in the database and WebUsers inside code to avoid conflicts between build in references to "User".
    /// </summary>
    [Table("Users")]
    public class WebUser
    {
        // Todo - the db tables have already been created for this, need to check them and add properties

        #region Properties

        /// <summary>
        /// The unique id for the database record.
        /// </summary>
        [Key]
        public Guid Guid { get; set; }

        /// <summary>
        /// The city the user is from.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// When the database record was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The country (abbreviation) the user is from.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The users registered email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The users first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Specifies whether the user is still active or not.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Specifies whether the user is locked out or not.
        /// </summary>
        public bool IsLockedOut { get; set; }

        /// <summary>
        /// The users last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// An optional phone number to contact the user.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Url of the users profile picture
        /// </summary>
        public string PictureUri { get; set; }

        /// <summary>
        /// The postal code for the registered address of the user.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// An optional province the user is located in.
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// The optional state (abbreviation) the user is located in.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The street address (1 of 2).
        /// </summary>
        public string Street1 { get; set; }

        /// <summary>
        /// The street address (2 of 2).
        /// </summary>
        public string Street2 { get; set; }

        /// <summary>
        /// A job title for the user (optional).
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// An alias or username for the user.
        /// </summary>
        public string Username { get; set; }

        #endregion

        #region Virtual Properties

        #endregion

        #region Calulcated Properties

        /// <summary>
        /// The concatenated full name (FirstName + " " + LastName).
        /// </summary>
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        /// <summary>
        /// The user address on one line.
        /// </summary>
        public string OneLineAddress
        {
            get
            {
                // check if state is specified (domestic primarily)
                if (!string.IsNullOrEmpty(this.State))
                {
                    return string.Format("{0} {1} {2}, {3} {4} {5}", new object[] { 
                        this.Street1, this.Street2, this.City, this.State, this.Country, this.PostalCode });
                }
                else
                {
                    return string.Format("{0} {1} {2}, {3} {4} ", new object[] { 
                        this.Street1, this.Street2, this.City, this.Country, this.PostalCode });
                }
            }
        }

        #endregion

        #region Methods

        #endregion
    }
}