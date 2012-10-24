using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    /// <summary>
    /// An educational asset.
    /// </summary>
    public class Asset
    {
        #region Properties

        /// <summary>
        /// The unique id for the database record.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The type of asset such as a blog post, open courseware, etc.
        /// </summary>
        [ForeignKey("AssetType")]
        public int AssetTypeId { get; set; }

        /// <summary>
        /// The id of the user who contributed the asset information.
        /// </summary>
        [ForeignKey("Contributor")]
        public Guid ContributorGuid { get; set; }

        /// <summary>
        /// The cost of the asset.
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// When the database record was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The description of the asset.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The rated difficulty of the asset.
        /// </summary>
        public AssetDifficulty? Difficulty { get; set; }

        /// <summary>
        /// The rated importance of the asset.
        /// </summary>
        public AssetImportance? Importance { get; set; }

        /// <summary>
        /// The path to a picture of the asset.
        /// </summary>
        public string PictureUri { get; set; }

        /// <summary>
        /// Whether the asset is active (not deleted) or not.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Whether completion of the asset will be scored.
        /// </summary>
        public bool IsScoreable { get; set; }

        /// <summary>
        /// Whether a test will be required to complete the asset.
        /// </summary>
        public bool IsTestRequired { get; set; }

        /// <summary>
        /// The score required to pass to complete the asset if a test is required.
        /// </summary>
        public int? PassingScore { get; set; }

        /// <summary>
        /// The title of the asset.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The web location of the asset.
        /// </summary>
        public string Uri { get; set; }

        #endregion

        #region Virtual Properties

        /// <summary>
        /// The type of asset.
        /// </summary>
        public virtual AssetType AssetType { get; set; }

        /// <summary>
        /// User comments associated with this educational asset.
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// The user who contributed this educational asset.
        /// </summary>
        public virtual WebUser Contributor { get; set; }

        /// <summary>
        /// Asset tags (subjects).
        /// </summary>
        public virtual ICollection<AssetToSubjectAssociation> SubjectAssociations { get; set; }

        #endregion

        #region Calculated Properties

        private int? _commentCount;

        [NotMapped]
        public int CommentCount
        {
            get
            {
                if (_commentCount != null)
                {
                    return _commentCount.Value;
                }
                else
                {
                    if (this.Comments != null)
                    {
                        return this.Comments.Count();
                    }
                }

                return 0;
            }
            set
            {
                _commentCount = value;
            }
        }

        [NotMapped]
        public int ImportanceAsPercent
        {
            get
            {
                int result = 0;

                switch (this.Importance)
                {
                    case AssetImportance.Low:
                        result = 25;
                        break;
                    case AssetImportance.Medium:
                        result = 50;
                        break;
                    case AssetImportance.High:
                        result = 75;
                        break;
                    case AssetImportance.Critical:
                        result = 100;
                        break;
                    case AssetImportance.Unspecified:
                    case AssetImportance.Irrelevant:
                    default:
                        result = 0;
                        break;
                }

                return result;
            }
        }

        [NotMapped]
        public int DifficultyAsPercent
        {
            get
            {
                int result = 0;

                switch (this.Difficulty)
                {
                    case AssetDifficulty.EntryLevel:
                        result = 20;
                        break;
                    case AssetDifficulty.Easy:
                        result = 40;
                        break;
                    case AssetDifficulty.Medium:
                        result = 60;
                        break;
                    case AssetDifficulty.Hard:
                        result = 80;
                        break;
                    case AssetDifficulty.Expert:
                        result = 100;
                        break;
                    case AssetDifficulty.Unspecified:
                    default:
                        result = 0;
                        break;
                }

                return result;
            }
        }

        [NotMapped]
        public int StudentCompletions { get; set; }

        #endregion

        #region Constructors

        public Asset()
        {
            this.Comments = new List<Comment>();
            this.SubjectAssociations = new List<AssetToSubjectAssociation>();
        }

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Asset return false.
            Asset other = obj as Asset;
            if ((System.Object)other == null)
            {
                return false;
            }

            // Check properties
            //return (
            //    this.AssetTypeId.Equals(other.AssetTypeId) &&
            //    this.ContributorGuid.Equals(other.ContributorGuid) &&
            //    this.Cost.Equals(other.Cost) &&
            //    this.Description.Equals(other.Description) &&
            //    this.Difficulty.Equals(other.Difficulty) &&
            //    this.Id.Equals(other.Id) &&
            //    this.Importance.Equals(other.Importance) &&
            //    this.IsActive.Equals(other.IsActive) &&
            //    this.IsScoreable.Equals(other.IsScoreable) &&
            //    this.IsTestRequired.Equals(other.IsTestRequired) &&
            //    this.PassingScore.Equals(other.PassingScore) &&
            //    this.Title.Equals(other.Title) &&
            //    this.Uri.Equals(other.Uri)
            //);

            // todo: double check this code later getting null object errors
            return true;
        }

        #endregion
    }

    /// <summary>
    /// The list of possible difficulty levels.
    /// </summary>
    public enum AssetDifficulty
    {
        Unspecified = 0,
        EntryLevel = 1,
        Easy = 2,
        Medium = 3,
        Hard = 4,
        Expert = 5
    }

    /// <summary>
    /// The list of possible importance levels.
    /// </summary>
    public enum AssetImportance
    {
        Unspecified = 0,
        Irrelevant = 1,
        Low = 2,
        Medium = 3,
        High = 4,
        Critical = 5
    }
}