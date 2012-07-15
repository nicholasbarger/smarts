using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public int AssetTypeId { get; set; }
        public Guid ContributorGuid { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
        public AssetDifficulty Difficulty { get; set; }
        public AssetImportance Importance { get; set; }
        public bool IsScoreable { get; set; }
        public bool IsTestRequired { get; set; }
        public int PassingScore { get; set; }
        public string Title { get; set; }
        public Uri Uri { get; set; }
    }

    public enum AssetDifficulty
    {
        EntryLevel = 1,
        Easy = 2,
        Medium = 3,
        Hard = 4,
        Expert = 5
    }

    public enum AssetImportance
    {
        Irrelevant = 1,
        Low = 2,
        Medium = 3,
        High = 4,
        Critical = 5
    }
}