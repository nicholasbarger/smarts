using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Smarts.Api.Models;

namespace Smarts.Api.Logic
{
    /// <summary>
    /// Put all validation rules across all objects here.
    /// A new overloaded method should be created accepting each type of model.
    /// </summary>
    internal class ValidationRules
    {
        /// <summary>
        /// Contains the list of errors during validation.
        /// </summary>
        public Dictionary<string, string> Errors { get; set; }
        
        /// <summary>
        /// Specifies whether the object is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (this.Errors.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Constructor to create new instance of validation rules.
        /// </summary>
        public ValidationRules()
        {
            this.Errors = new Dictionary<string, string>();
        }

        //NOTE: All validation logic is nestled in here across all objects, we may want to refactor this depending on how big it becomes and if it is a maintenance issue
        #region Model Validations

        /// <summary>
        /// Asset validation
        /// </summary>
        /// <param name="obj"></param>
        public void Validate(Asset obj)
        {
            if (obj == null)
            {
                this.Errors.Add("00004", Resources.Errors.ERR00004);
                return;
            }

            if (obj.AssetTypeId <= 0)
            {
                this.Errors.Add("00105", Resources.Errors.ERR00105);
            }

            if (obj.ContributorGuid == null || string.IsNullOrWhiteSpace(obj.ContributorGuid.ToString()))
            {
                this.Errors.Add("00106", Resources.Errors.ERR00106);
            }

            if (obj.Difficulty != AssetDifficulty.Unspecified && ((int)obj.Difficulty < 1 || (int)obj.Difficulty > 5))
            {
                this.Errors.Add("00107", Resources.Errors.ERR00107);
            }

            if (obj.Importance != AssetImportance.Unspecified && ((int)obj.Importance < 1 || (int)obj.Importance > 5))
            {
                this.Errors.Add("00108", Resources.Errors.ERR00108);
            }

            if (obj.PassingScore.HasValue && (obj.PassingScore <= 0 || obj.PassingScore > 100))
            {
                this.Errors.Add("00109", Resources.Errors.ERR00109);
            }

            if (string.IsNullOrWhiteSpace(obj.Title) || obj.Title.Length > 100)
            {
                this.Errors.Add("00110", Resources.Errors.ERR00110);
            }

            if(obj.Uri == null || string.IsNullOrWhiteSpace(obj.Uri.ToString()))
            {
                this.Errors.Add("00111", Resources.Errors.ERR00111);
            }
            else if(obj.Uri.Length > 50)
            {
                this.Errors.Add("00112", Resources.Errors.ERR00112);
            }
        }

        public void Validate(Curriculum obj)
        {
            if (obj == null)
            {
                this.Errors.Add("00004", Resources.Errors.ERR00004);
                return;
            }

            if (obj.ContributorGuid == null || obj.ContributorGuid == Guid.Empty)
            {
                this.Errors.Add("00106", Resources.Errors.ERR00106);
            }

            if (obj.Description.Length > 4000)
            {
                this.Errors.Add("00301", Resources.Errors.ERR00301);
            }

            if (obj.Title.Length > 100)
            {
                this.Errors.Add("00302", Resources.Errors.ERR00302);
            }
        }

        /// <summary>
        /// Subject validation
        /// </summary>
        /// <param name="obj"></param>
        public void Validate(Subject obj)
        {
            if (obj == null)
            {
                this.Errors.Add("00004", Resources.Errors.ERR00004);
                return;
            }

            if (string.IsNullOrEmpty(obj.Hashtag))
            {
                this.Errors.Add("00200", Resources.Errors.ERR00200);
            }

            if (obj.ContributorGuid == null || obj.ContributorGuid == Guid.Empty)
            {
                this.Errors.Add("00106", Resources.Errors.ERR00106);
            }

            if (obj.Description.Length > 4000)
            {
                this.Errors.Add("00201", Resources.Errors.ERR00201);
            }

            if (obj.Title.Length > 100)
            {
                this.Errors.Add("00202", Resources.Errors.ERR00202);
            }
        }

        /// <summary>
        /// Webuser validation
        /// </summary>
        /// <param name="obj"></param>
        public void Validate(WebUser obj)
        {
            if (obj == null)
            {
                this.Errors.Add("00004", Resources.Errors.ERR00004);
                return;
            }

            // TODO: add the rest of the validation logic here for webuser (user)
        }

        #endregion
    }
}