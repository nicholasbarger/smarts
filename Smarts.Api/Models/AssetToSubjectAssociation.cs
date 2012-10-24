using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    [Table("MapAssetToSubject")]
    public class AssetToSubjectAssociation
    {
        [Key, Column(Order = 0), ForeignKey("Asset")]
        public int AssetId { get; set; }

        [Key, Column(Order = 1), ForeignKey("Subject")]
        public string Hashtag { get; set; }

        [ForeignKey("Contributor")]
        public Guid ContributorGuid { get; set; }

        public DateTime Created { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual WebUser Contributor { get; set; }
    }
}