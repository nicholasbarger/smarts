using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Smarts.Api.Models;

namespace Smarts.Api.Logic
{
    public class ValidationRules
    {
        public Dictionary<string, string> Errors { get; set; }
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

        public ValidationRules()
        {
            this.Errors = new Dictionary<string, string>();
        }

        public void Validate(Asset obj)
        {
            if (obj == null)
            {
                this.Errors.Add("001", "The asset is null.");
                return;
            }

            if (obj.AssetTypeId <= 0)
            {
                this.Errors.Add("002", "The asset type must be specified.");
            }

            if (obj.ContributorGuid == null || string.IsNullOrWhiteSpace(obj.ContributorGuid.ToString()))
            {
                this.Errors.Add("003", "The contributor must be specified.");
            }

            if (obj.Difficulty != AssetDifficulty.Unspecified && ((int)obj.Difficulty < 1 || (int)obj.Difficulty > 5))
            {
                this.Errors.Add("004", "Difficulty rating must be between 1 and 5 if specified.");
            }

            if (obj.Importance != AssetImportance.Unspecified && ((int)obj.Importance < 1 || (int)obj.Importance > 5))
            {
                this.Errors.Add("005", "Importance rating must be between 1 and 5 if specified.");
            }

            if (obj.PassingScore.HasValue && (obj.PassingScore <= 0 || obj.PassingScore > 100))
            {
                this.Errors.Add("006", "Passing score must be between 1 and 100 if specified.");
            }

            if (string.IsNullOrWhiteSpace(obj.Title) || obj.Title.Length > 100)
            {
                this.Errors.Add("007", "Asset title must be specified and can not exceed 100 characters.");
            }

            if(obj.Uri == null || string.IsNullOrWhiteSpace(obj.Uri.ToString()))
            {
                this.Errors.Add("008", "Asset uri is required.");
            }
            else if(obj.Uri.Length > 50)
            {
                this.Errors.Add("009", "The specified uri is too long, please use a url shortening tool such as http://www.bit.ly");
            }
        }
    }
}