using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Smarts.Api.Models;

namespace Smarts.Api.Logic
{
    public class ValidationRules
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

        public void Validate(Asset obj)
        {
            if (obj == null)
            {
                this.Errors.Add("00004", string.Format(Resources.Errors.ERR00004, "asset"));
                return;
            }

            if (obj.AssetTypeId <= 0)
            {
                this.Errors.Add("00005", Resources.Errors.ERR00005);
            }

            if (obj.ContributorGuid == null || string.IsNullOrWhiteSpace(obj.ContributorGuid.ToString()))
            {
                this.Errors.Add("00006", Resources.Errors.ERR00006);
            }

            if (obj.Difficulty != AssetDifficulty.Unspecified && ((int)obj.Difficulty < 1 || (int)obj.Difficulty > 5))
            {
                this.Errors.Add("00007", Resources.Errors.ERR00007);
            }

            if (obj.Importance != AssetImportance.Unspecified && ((int)obj.Importance < 1 || (int)obj.Importance > 5))
            {
                this.Errors.Add("00008", Resources.Errors.ERR00008);
            }

            if (obj.PassingScore.HasValue && (obj.PassingScore <= 0 || obj.PassingScore > 100))
            {
                this.Errors.Add("00009", Resources.Errors.ERR00009);
            }

            if (string.IsNullOrWhiteSpace(obj.Title) || obj.Title.Length > 100)
            {
                this.Errors.Add("00010", Resources.Errors.ERR00010);
            }

            if(obj.Uri == null || string.IsNullOrWhiteSpace(obj.Uri.ToString()))
            {
                this.Errors.Add("00011", Resources.Errors.ERR00011);
            }
            else if(obj.Uri.Length > 50)
            {
                this.Errors.Add("00012", Resources.Errors.ERR00012);
            }
        }

        #endregion
    }
}