using Smarts.Api.BusinessLogic;
using Smarts.Api.Db;
using Smarts.Api.Models;
using Smarts.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Smarts.Api.AppLogic
{
    public class WebUserAppLogic
    {
        WebUserBusinessLogic business;

        public WebUserAppLogic()
        {
            // create reference to business logic
            business = new WebUserBusinessLogic();
        }

        public Payload<WebUser> Get(Guid guid)
        {
            // create payload
            var payload = new Payload<WebUser>();

            // Validate
            var rules = new ValidationRules();
            rules.ValidateGuid(guid);

            // check if valid
            if (rules.IsValid)
            {
                // get user
                using(var queries = new WebUserQueries())
                {
                    payload.Data = queries.Get(guid);
                }
            }

            // todo: next step in workflow

            // return payload
            return payload;
        }

        public Payload<WebUser> Login(string username, string email, string password)
        {
            // create payload
            var payload = new Payload<WebUser>();

            // todo: check security

            // Validate
            var rules = new ValidationRules();
            rules.ValidateLoginEvent(username, email, password);

            // assign errors from validation (if applicable)
            payload.AssignValidationErrors(rules.Errors);

            // check if valid
            if (rules.IsValid)
            {
                // hash password
                var securityUtility = new SecurityUtilities();
                var hashedPassword = securityUtility.HashSomething(password);

                // get user based on email/username and hashed password
                WebUser user = null;
                using (var queries = new WebUserQueries())
                {
                    user = queries.GetByLogin(username, email);
                }
                
                // check if user is found (empty)
                if (user != null)
                {
                    // compare passwords to verify login
                    if (hashedPassword == user.HashedPassword)
                    {
                        // valid, so assign payload
                        payload.Data = user;

                        // log activity
                        AuditUtilities.Log(user, ActivityEventItem.Login, 
                            string.Format(Resources.AuditEntries.Login, username));
                    }
                    else
                    {
                        // password mismatch error
                        payload.Errors.Add("00404", Resources.Errors.ERR00404);

                        // log activity
                        AuditUtilities.Log(null, ActivityEventItem.LoginFailed,
                            string.Format(Resources.AuditEntries.LoginFailed, username, Resources.Errors.ERR00404));
                    }
                }
                else
                {
                    // throw error on not found user
                    payload.Errors.Add("00405", Resources.Errors.ERR00405);

                    // log activity
                    AuditUtilities.Log(null, ActivityEventItem.LoginFailed,
                        string.Format(Resources.AuditEntries.LoginFailed, username, Resources.Errors.ERR00405));
                }
            }

            // todo: next steps in workflow

            // return payload
            return payload;
        }

        public Payload<bool> Logout(WebUser obj)
        {
            // create payload
            var payload = new Payload<bool>();

            // todo: add more content around this to toggle logouts from db
            payload.Data = true;

            // based on a successful logout, log activity
            AuditUtilities.Log(obj, ActivityEventItem.Logout, 
                string.Format(Resources.AuditEntries.Logout, obj.Username));

            // return payload
            return payload;
        }

        public Payload<WebUser> Save(WebUser obj)
        {
            // create payload
            var payload = new Payload<WebUser>();

            // todo: check security

            // Prep obj
            bool isNewUser = (obj.Guid == null || obj.Guid == Guid.Empty);
            business.SetDefaults(ref obj);

            // hash password
            var securityUtility = new SecurityUtilities();
            obj.HashedPassword = securityUtility.HashSomething(obj.Password);

            // validate
            var rules = new ValidationRules();
            rules.Validate(obj);

            // assign errors from validation (if applicable)
            payload.AssignValidationErrors(rules.Errors);

            // check if valid
            if (rules.IsValid)
            {
                // if existing user, check the properties that have changed prior to update
                var changedProperties = new StringBuilder();
                bool isChangedPassword = false;
                if (!isNewUser)
                {
                    var originalUser = Get(obj.Guid).Data;
                    CheckChangedProperties(originalUser, obj, ref changedProperties);
                    isChangedPassword = CheckChangedPassword(originalUser, obj);
                }

                // save to db
                using (var queries = new WebUserQueries())
                {
                    queries.Save(ref obj);
                }

                // assign primary data
                payload.Data = obj;

                // log activity
                if (isNewUser)
                {
                    // new user
                    AuditUtilities.Log(obj, ActivityEventItem.Enroll, 
                        string.Format(Resources.AuditEntries.Enroll, obj.Username));
                }
                else
                {
                    // updated user
                    AuditUtilities.Log(obj, ActivityEventItem.ProfileUpdated,
                        string.Format(Resources.AuditEntries.ProfileUpdated, obj.Username, changedProperties));

                    // update if password was changed
                    if (isChangedPassword)
                    {
                        AuditUtilities.Log(obj, ActivityEventItem.PasswordChanged,
                            string.Format(Resources.AuditEntries.PasswordChanged, obj.Username));
                    }
                }
            }

            // todo: next steps in workflow

            // return payload
            return payload;
        }

        /// <summary>
        /// Compare two instances of a web user to see if the password has been changed.
        /// </summary>
        /// <param name="original"></param>
        /// <param name="updated"></param>
        /// <returns></returns>
        private bool CheckChangedPassword(WebUser original, WebUser updated)
        {
            return updated.HashedPassword.Equals(original.HashedPassword);
        }

        /// <summary>
        /// Compare two instances of a web user and specify their differences.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="changedProperties"></param>
        private void CheckChangedProperties(WebUser original, WebUser updated, ref StringBuilder changedProperties)
        {
            if (original.City != updated.City)
            {
                changedProperties.AppendFormat("Original city: {0}, updated city: {1}\n", original.City, updated.City);
            }

            if (original.Country != updated.Country)
            {
                changedProperties.AppendFormat("Original country: {0}, updated country: {1}\n", original.Country, updated.Country);
            }

            if (original.Email != updated.Email)
            {
                changedProperties.AppendFormat("Original email: {0}, updated email: {1}\n", original.Email, updated.Email);
            }

            if (original.FirstName != updated.FirstName)
            {
                changedProperties.AppendFormat("Original first name: {0}, updated first name: {1}\n", original.FirstName, updated.FirstName);
            }

            if (original.IsActive != updated.IsActive)
            {
                changedProperties.AppendFormat("Original active: {0}, updated active: {1}\n", original.IsActive, updated.IsActive);
            }

            if (original.IsLockedOut != updated.IsLockedOut)
            {
                changedProperties.AppendFormat("Original locked out: {0}, updated locked out: {1}\n", original.IsLockedOut, updated.IsLockedOut);
            }

            if (original.LastName != updated.LastName)
            {
                changedProperties.AppendFormat("Original last name: {0}, updated last name: {1}\n", original.LastName, updated.LastName);
            }

            if (original.Phone != updated.Phone)
            {
                changedProperties.AppendFormat("Original phone: {0}, updated phone: {1}\n", original.Phone, updated.Phone);
            }

            if (original.PictureUri != updated.PictureUri)
            {
                changedProperties.AppendFormat("Original picture: {0}, updated picture: {1}\n", original.PictureUri, updated.PictureUri);
            }

            if (original.PostalCode != updated.PostalCode)
            {
                changedProperties.AppendFormat("Original postal code: {0}, updated postal code: {1}\n", original.PostalCode, updated.PostalCode);
            }

            if (original.Province != updated.Province)
            {
                changedProperties.AppendFormat("Original province: {0}, updated province: {1}\n", original.Province, updated.Province);
            }

            if (original.State != updated.State)
            {
                changedProperties.AppendFormat("Original state: {0}, updated state: {1}\n", original.State, updated.State);
            }

            if (original.Street1 != updated.Street1)
            {
                changedProperties.AppendFormat("Original street 1: {0}, updated street 1: {1}\n", original.Street1, updated.Street1);
            }

            if (original.Street2 != updated.Street2)
            {
                changedProperties.AppendFormat("Original street 2: {0}, updated street 2: {1}\n", original.Street2, updated.Street2);
            }

            if (original.Title != updated.Title)
            {
                changedProperties.AppendFormat("Original title: {0}, updated title: {1}\n", original.Title, updated.Title);
            }

            if (original.Username != updated.Username)
            {
                changedProperties.AppendFormat("Original username: {0}, updated username: {1}\n", original.Username, updated.Username);
            }
        }
    }
}