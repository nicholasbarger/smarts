using Smarts.Api.BusinessLogic;
using Smarts.Api.Db;
using Smarts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                // todo: hash password
                var hashedPassword = password;

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
                    }
                    else
                    {
                        // password mismatch error
                        payload.Errors.Add("00404", Resources.Errors.ERR00404);
                    }
                }
                else
                {
                    // throw error on not found user
                    payload.Errors.Add("00405", Resources.Errors.ERR00405);
                }
            }

            // todo: next steps in workflow

            // return payload
            return payload;
        }

        public Payload<WebUser> Save(WebUser obj)
        {
            // create payload
            var payload = new Payload<WebUser>();

            // todo: check security

            // Prep obj
            business.SetDefaults(ref obj);

            // validate
            var rules = new ValidationRules();
            rules.Validate(obj);

            // assign errors from validation (if applicable)
            payload.AssignValidationErrors(rules.Errors);

            // check if valid
            if (rules.IsValid)
            {
                // save to db
                using (var queries = new WebUserQueries())
                {
                    queries.Save(ref obj);
                }

                // assign primary data
                payload.Data = obj;
            }

            // todo: next steps in workflow

            // return payload
            return payload;
        }
    }
}